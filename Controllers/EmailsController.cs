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
}