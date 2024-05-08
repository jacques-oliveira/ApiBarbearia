using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class AuthController :ControllerBase{
    private readonly ITokenService _tokenService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;

    public AuthController(ITokenService tokenService, 
                            UserManager<ApplicationUser> userManager, 
                            RoleManager<IdentityRole> roleManager, 
                            IConfiguration configuration)
    {
        _tokenService = tokenService;
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
    }

    [HttpPost]
    [Route("login")]
    public async Task<ActionResult> Login([FromBody] LoginModelDTO model){
        var user = await _userManager.FindByNameAsync(model.UserName!);

        if(user is not null && await _userManager.CheckPasswordAsync(user, model.Password!)){

            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>{
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach(var userRole in userRoles){

                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var token = _tokenService.GenerateAccessToken(authClaims,_configuration);

            var refreshToken = _tokenService.GenerateRefreshToken();
            _= int.TryParse(_configuration["JWT:RefreshTokenValidityInMinutes"],
                                out int RefreshTokenValidityInMinutes);

            user.RefreshTokenExpiryTime = DateTime.Now.AddMinutes(RefreshTokenValidityInMinutes);

            user.RefreshToken = refreshToken;

            await _userManager.UpdateAsync(user);     

            return Ok(new{
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                refreshToken = refreshToken,
                Expiration = token.ValidTo
            });                           

        }
        return Unauthorized();
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModelDTO model){

        var userExists = await _userManager.FindByNameAsync(model.UserName!);

        if(userExists != null){
            return StatusCode(StatusCodes.Status500InternalServerError,
                    new ResponseDTO { Status = "Error", Message = "Usuário já existe!"});
        }

        ApplicationUser user = new(){
            Email = model.Email,
            SecurityStamp =  Guid.NewGuid().ToString(),
            UserName = model.UserName
        };

        var result = await _userManager.CreateAsync(user, model.Password!);

        if(!result.Succeeded){
            return StatusCode(StatusCodes.Status500InternalServerError,
                    new ResponseDTO { Status = "Error", Message = "Criação do usuário falhou."});
        }

        return Ok(new ResponseDTO { Status = "Sucessso", Message = "Usuário criado com sucesso!"});
    }

    [HttpPost]
    [Route("refresh-token")]
    public async Task<IActionResult> RefreshToken(TokenModelDTO tokenModel){
        if(tokenModel is null){
            return BadRequest("Solicitação pelo cliente inválida!");
        }

        string? accessToken =  tokenModel.AccessToken
                                ?? throw new ArgumentNullException(nameof(tokenModel));
        string? refreshToken = tokenModel.RefreshToken
                                ?? throw new ArgumentException(nameof(tokenModel));

        var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken!, _configuration);

        if(principal == null){

            return BadRequest("Token de acesso inválido!");
        } 

        string userName = principal.Identity.Name;

        var user = await _userManager.FindByEmailAsync(userName!);

        if(user == null || user.RefreshToken != refreshToken
                        || user.RefreshTokenExpiryTime <= DateTime.Now)
        {
            return BadRequest("Token de acesso inválido!");
        }

        var newAccessToken = _tokenService.GenerateAccessToken(
                            principal.Claims.ToList(), _configuration);

        var newRefreshToken = _tokenService.GenerateRefreshToken();
        
        user.RefreshToken =  newRefreshToken;

        await _userManager.UpdateAsync(user);

        return new ObjectResult(new
        {
                accessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                refreshToken = newRefreshToken
        });

    }
}