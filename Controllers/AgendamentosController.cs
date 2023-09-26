using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("[controller]")]
public class AgendamentosController : ControllerBase{

private readonly AppDbContext _context;

    public AgendamentosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Agendamento>> Get(){
        var agendamentos = _context.Agendamentos.AsNoTracking().ToList();

        if(agendamentos is null){
            return BadRequest("Agendamento não enconrado!");
        }
        return agendamentos;
    }
}