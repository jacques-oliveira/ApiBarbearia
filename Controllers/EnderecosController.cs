using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("[controller]")]
public class EnderecosController : ControllerBase{

    private readonly IUnityOfWork _uow;

    public EnderecosController(IUnityOfWork context)
    {
        _uow = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Endereco>> Get(){
        
        var enderecos = _uow.EnderecoRepository.Get().ToList();

        if(enderecos is null){
            return NotFound("Não existem endereços cadastrados");
        }

        return enderecos;
    }

    [HttpGet("{id:int}",Name ="ObterEndereco")]
    public async Task<ActionResult<Endereco>> Get(int id){

        var endereco = await _uow.EnderecoRepository.GetById(e => e.EnderecoId == id);

        if(id != endereco?.EnderecoId){
            return NotFound("Endereço não encontrado!");
        }

        return endereco;
    }

    [HttpPost]
    public async Task<ActionResult> Post(Endereco endereco){
        if(endereco is null){
            return BadRequest();
        }

        _uow.EnderecoRepository.Add(endereco);
        await _uow.Commit();

        return new CreatedAtRouteResult("ObterEndereco",
            new {id = endereco.EnderecoId, endereco});
        
    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Endereco endereco){

        if(id != endereco.EnderecoId){
            return NotFound("Endereço não encontrado");
        }

        _uow.EnderecoRepository.Update(endereco);
        _uow.Commit();

        return Ok(endereco);
    }
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id){
        var endereco = await _uow.EnderecoRepository.GetById(e => e.EnderecoId == id);

        if(endereco is null){
            return NotFound("Endereço não encontrado");
        }

        _uow.EnderecoRepository.Delete(endereco);
        await _uow.Commit();

        return Ok(endereco);
    }
}