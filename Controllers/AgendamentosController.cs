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
    public async Task<ActionResult<IEnumerable<AgendamentoDTO>>> Get(){

        var agendamentos = await _uow.AgendamentoRepository.GetAgendamentoUsuario();            
        
        if(agendamentos is null){
            return BadRequest("Agendamento não enconrado!");
        }
        
        List<AgendamentoDTO> agendamentoDTOs = new List<AgendamentoDTO>();
        foreach(var agendamento in agendamentos){
            agendamentoDTOs.Add(new AgendamentoDTO{
                DataAgendamento = agendamento.Data,
                NomeUsuario = agendamento.Usuarios.Nome,
                NomeProduto = agendamento.Produtos.Nome,
                DescricaoProduto = agendamento.Produtos.Descricao,
                PrecoProduto = agendamento.Produtos.Preco
            });
        }
        return Ok(agendamentoDTOs.OrderBy(a => a.DataAgendamento));
    }

    [HttpGet("{id:int}",Name ="ObterAgendamento")]
    public async Task<ActionResult<Agendamento>> Get(int id){

        var agendamento = await _uow.AgendamentoRepository.Get().Include(u => u.Usuarios).FirstOrDefaultAsync(a => a.AgendamentoId == id);

        if(agendamento is null){
            return NotFound("Agendamento não econtrado!");
        }

        return Ok(agendamento);
    }

    [HttpPost]
    public async Task<ActionResult> Post(Agendamento agendamento){

        if(agendamento is null){

            return BadRequest();
        }

        _uow.AgendamentoRepository.Add(agendamento);
        await _uow.Commit();

        return new CreatedAtRouteResult("ObterAgendamento",
            new {id = agendamento.AgendamentoId, agendamento});
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Put(int id, Agendamento agendamento) {

        if(id != agendamento.AgendamentoId){
            return NotFound();
        }

        _uow.AgendamentoRepository.Update(agendamento);
        await _uow.Commit();

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