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
        var agendamentos = _context.Agendamentos.
            Include(u=> u.Usuarios).
            Include(p=> p.Produtos).AsNoTracking().ToList();

        if(agendamentos is null){
            return BadRequest("Agendamento não enconrado!");
        }
        return agendamentos;
    }

    [HttpGet("{id:int}",Name ="ObterAgendamento")]
    public ActionResult<Agendamento> Get(int id){
        var agendamento = _context.Agendamentos.Include(u => u.Usuarios).FirstOrDefault(a => a.AgendamentoId == id);

        if(agendamento is null){
            return NotFound("Agendamento não econtrado!");
        }

        return agendamento;
    }

    [HttpPost]
    public ActionResult Post(Agendamento agendamento){

        if(agendamento is null){
            return BadRequest();
        }

        _context.Agendamentos.Add(agendamento);
        _context.SaveChanges();

        return new CreatedAtRouteResult("ObterAgendamento",
            new {id = agendamento.AgendamentoId, agendamento});
    }
}