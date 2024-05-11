using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Authorize(AuthenticationSchemes = "Bearer")]
[ApiController]
[Route("[controller]")]
public class AgendamentosController : ControllerBase{

private readonly IUnityOfWork _uow;

    public AgendamentosController(IUnityOfWork context)
    {
        _uow = context;
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Agendamento>>> Get(){

        var agendamentos = await _uow.AgendamentoRepository.Get().ToListAsync();            

        if(agendamentos is null){
            return BadRequest("Agendamento não enconrado!");
        }

        return Ok(agendamentos);
    }

    [HttpGet("{id:int}",Name ="ObterAgendamento")]
    public ActionResult<Agendamento> Get(int id){
        var agendamento = _uow.AgendamentoRepository.Get().Include(u => u.Usuarios).FirstOrDefault(a => a.AgendamentoId == id);

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

        _uow.AgendamentoRepository.Add(agendamento);
        _uow.Commit();

        return new CreatedAtRouteResult("ObterAgendamento",
            new {id = agendamento.AgendamentoId, agendamento});
    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Agendamento agendamento) {

        if(id != agendamento.AgendamentoId){
            return NotFound();
        }

        _uow.AgendamentoRepository.Update(agendamento);
        _uow.Commit();

        return Ok(agendamento);

    }

    [HttpDelete("id:int")]
    public async Task<ActionResult> Delete(int id){

        var agendamento = await _uow.AgendamentoRepository.GetById(a => a.AgendamentoId == id);

        if(agendamento is null){
            return NotFound("Agendamento não econtrado!");
        }

        _uow.AgendamentoRepository.Delete(agendamento);
        await _uow.Commit();

        return Ok(agendamento);
    }
}