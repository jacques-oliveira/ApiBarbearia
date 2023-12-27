using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("[controller]")]
public class EmailsController : ControllerBase{

    private readonly IUnityOfWork _uow;

    public EmailsController(IUnityOfWork context)
    {
        _uow = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Email>> Get(){
        
        var emails = _uow.EmailRepository.Get().ToList();

        if(emails is null){
            return NotFound("Emails não econtrado");
        }

        return emails;
    }

    [HttpGet("{id:int}")]
    public ActionResult<Email> Get(int id){
        
        try{

            var email = _uow.EmailRepository.GetById(e => e.EmailId == id);

            if(email is null){
                return NotFound("Email não econtrado!");
            }

            return Ok(email);

        }catch(Exception){
            return StatusCode(StatusCodes.Status500InternalServerError,
            "Ocorreu um erro ao tratar a solicitação");
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
}