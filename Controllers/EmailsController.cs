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
}