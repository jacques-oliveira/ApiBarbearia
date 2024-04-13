using System.Text.Json;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("[controller]")]
public class UsuariosController : ControllerBase{
    private readonly IUnityOfWork _uow;
    private readonly IMapper _mapper;
    public UsuariosController(IUnityOfWork context, IMapper mapper)
    {
        _uow = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UsuarioDTO>>> Get([FromQuery] UsuariosParameters usuariosParameters){
        var usuarios = await _uow.UsuarioRepository.GetUsuarios(usuariosParameters);

        var metadata = new {
            usuarios.TotalCount,
            usuarios.PageSize,
            usuarios.CurrentPage,
            usuarios.TotalPages,
            usuarios.HasNext,
            usuarios.HasPrevious
        };

        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metadata));
        
        if(usuarios is null){
            return BadRequest("Usuários não encontrados");
        }

        var usuariosDto = _mapper.Map<List<UsuarioDTO>>(usuarios);
        return usuariosDto;
    }

    [HttpGet("{id:int}",Name ="ObterUsuario")]
    public async Task<ActionResult<UsuarioDTO>> Get(int id){
        var usuario = await _uow.UsuarioRepository.GetById(u => u.UsuarioId == id);

        if(usuario is null){
            return NotFound("Usuario não encontrado!");
        }

        var usuarioDto = _mapper.Map<UsuarioDTO>(usuario);
        return Ok(usuarioDto);
    }

    [HttpGet("dados")]
    public async Task<ActionResult<IEnumerable<UsuarioDTO>>> GetDadosUsuario(){
        var dadosUsuario = await _uow.UsuarioRepository.GetDadosUsuarios();

        if(dadosUsuario is null){
            return BadRequest("Dados Não Encontrados!");
        }
        var dadosUsuarioDto = _mapper.Map<List<UsuarioDTO>>(dadosUsuario);
        return dadosUsuarioDto;
    }

    [HttpPost]
    public async Task<ActionResult> Post(UsuarioDTO usuarioDto){

        if(usuarioDto is null){
            return BadRequest();
        }

        var usuario = _mapper.Map<Usuario>(usuarioDto);

        _uow.UsuarioRepository.Add(usuario);
        await _uow.Commit();        

        return new CreatedAtRouteResult("ObterUsuario",
            new { id = usuario.UsuarioId, usuario});
    }

    [HttpPut("{id:int}")]
    public ActionResult<UsuarioDTO> Put(int id, UsuarioDTO usuarioDto){
        
        if(id != usuarioDto.UsuarioId){
            return NotFound("O usuário não existe!");
        }

        var usuario = _mapper.Map<Usuario>(usuarioDto);

        _uow.UsuarioRepository.Update(usuario);
        _uow.Commit();

        return Ok(usuarioDto);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id){
        var usuario = await _uow.UsuarioRepository.GetById(u => u.UsuarioId == id);

        if(usuario is null){
            return BadRequest("Usuario não encontrado!");
        }

        _uow.UsuarioRepository.Delete(usuario);
        await _uow.Commit();

        var usuarioDto = _mapper.Map<UsuarioDTO>(usuario);
        
        return Ok(usuarioDto);
    }
}