using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("[controller]")]
public class EnderecosController : ControllerBase{

    private readonly AppDbContext _context;

    public EnderecosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Endereco>> Get(){
        
        var enderecos = _context.Enderecos.AsNoTracking().ToList();

        if(enderecos is null){
            return NotFound("Não existem endereços cadastrados");
        }

        return enderecos;
    }

    [HttpGet("{id:int}",Name ="ObterEndereco")]
    public ActionResult<Endereco> Get(int id){

        var endereco = _context.Enderecos.FirstOrDefault(e => e.EnderecoId == id);

        if(id != endereco?.EnderecoId){
            return NotFound("Endereço não encontrado!");
        }

        return endereco;
    }

    [HttpPost]
    public ActionResult Post(Endereco endereco){
        if(endereco is null){
            return BadRequest();
        }

        _context.Enderecos.Add(endereco);
        _context.SaveChanges();

        return new CreatedAtRouteResult("ObterEndereco",
            new {id = endereco.EnderecoId, endereco});
        
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id){
        var endereco = _context.Enderecos.FirstOrDefault(e => e.EnderecoId == id);

        if(endereco is null){
            return NotFound("Endereço não encontrado");
        }

        _context.Remove(endereco);
        _context.SaveChanges();

        return Ok(endereco);
    }
}