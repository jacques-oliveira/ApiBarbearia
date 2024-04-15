using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("[controller]")]
public class EmailsController : ControllerBase{

    private readonly IUnityOfWork _uow;
    private readonly ILogger<EmailsController> _logger;

    public EmailsController(IUnityOfWork context,ILogger<EmailsController> logger)
    {
        _uow = context;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Email>>> Get(){
        
        var emails = await _uow.EmailRepository.Get().ToListAsync();

        if(emails is null){
            
            return NotFound("Emails não econtrado");
        }

        return Ok(emails);
    }

    [HttpGet("{id:int}",Name ="ObterEmail")]
    public async Task<ActionResult<Email>> Get(int id){
        
        try{

            var email = await _uow.EmailRepository.GetById(e => e.EmailId == id);

            if(email is null){
                return NotFound("Email não econtrado!");
            }

            return Ok(email);

        }catch(Exception){
            return StatusCode(StatusCodes.Status500InternalServerError,
            "Ocorreu um erro ao tratar a solicitação");
        }    
    }

    [HttpPost]
    public async Task<ActionResult> Post(Email email){

        try{
            if(email is null){

                return BadRequest();
            }
            _uow.EmailRepository.Add(email);
            await _uow.Commit();

                    return new CreatedAtRouteResult("ObterEmail",
            new {id = email.EmailId,email});
            
        }catch(Exception ex){

            return StatusCode(500);
        }
    }

    [HttpPut("{id:int:min(1)}")]
    public async Task<ActionResult> Put(int id, Email email){

        try{

            if (id != email.EmailId)
            {
                return BadRequest("Email não econtrado");
            }

            var emailDB  = await _uow.EmailRepository.GetById(e=> e.EmailId == id);

            if(emailDB is null){
                return NotFound("");
            }
            
            emailDB.EnderecoEmail = email.EnderecoEmail;
            _uow.EmailRepository.Update(emailDB);
            _uow.Commit();

            return Ok(email);

        }
        catch(Exception){
            return StatusCode(StatusCodes.Status500InternalServerError,
                                "Ocorreu um erro ao tratar sua solicitação");
        }      


    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id){

        try{

            var email = await _uow.EmailRepository.GetById(e=> e.EmailId == id);

            if(email is null){
                
                return NotFound("O email não existe!");
            }

            _uow.EmailRepository.Delete(email);
            await _uow.Commit();

            return Ok(email);

        }catch(Exception ex){

            return StatusCode(StatusCodes.Status404NotFound);
        }
    }
}