using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("[controller]")]
public class EmailsController : ControllerBase{

    private readonly AppDbContext _context;

    public EmailsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Email>> Get(){
        
        var emails = _context.Emails.AsTracking().ToList();

        if(emails is null){
            return NotFound("Emails não econtrado");
        }

        return emails;
    }

    [HttpGet("{id:int}")]
    public ActionResult<Email> Get(int id){
        
        try{

            var email = _context.Emails.FirstOrDefault(e => e.EmailId == id);

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
    public ActionResult Put(int id, Email email){

        try{

            if (id != email.EmailId)
            {
                return BadRequest("Email não econtrado");
            }

            var emailDB  = _context.Emails.Find(id);

            if(emailDB is null){
                return NotFound("");
            }

            _context.Emails.Entry(emailDB).State = EntityState.Modified;
            emailDB.EnderecoEmail = email.EnderecoEmail;
            _context.SaveChanges();

            return Ok(email);

        }
        catch(Exception){
            return StatusCode(StatusCodes.Status500InternalServerError,
                                "Ocorreu um erro ao tratar sua solicitação");
        }      


    }
}