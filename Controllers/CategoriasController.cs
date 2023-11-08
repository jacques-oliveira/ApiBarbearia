using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("[controller]")]
public class CategoriasController : ControllerBase{
    private readonly AppDbContext _context;

    public CategoriasController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Categoria>> Get(){

        try{

            return _context.Categorias.AsNoTracking().ToList();

        }catch(Exception){

            return StatusCode(StatusCodes.Status500InternalServerError,
            "Ocorreu um problema ao tratar sua solicitação.");
        }
        
    }

    [HttpGet("produtos")]
    public ActionResult<IEnumerable<Categoria>> GetCategoriaProduto(){
        try{
            return _context.Categorias.Include(p=> p.Produtos).Take(5).ToList();
        }catch(Exception){
            return StatusCode(StatusCodes.Status500InternalServerError,
            "Ocorreu um problema ao tratar sua solicitação.");
        }
        
    }

    [HttpGet("{id:int}")]
    public ActionResult<Categoria> Get(int id){

        try{
            var categoria = _context.Categorias.FirstOrDefault(c => c.CategoriaId == id);

            if(categoria is null){
                return NotFound("Categoria não encontrada");
            }

            return Ok(categoria);

        }catch(Exception){
            return StatusCode(StatusCodes.Status500InternalServerError,
            "Ocorreu um problema ao tratar sua solicitação.");
        }

    }


    [HttpPost]
    public ActionResult Post(Categoria categoria){
        if(categoria is null){
            return BadRequest();
        }

        _context.Categorias.Add(categoria);
        _context.SaveChanges();

        return new CreatedAtRouteResult("ObterCategoria",
            new {id = categoria.CategoriaId, categoria});
    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Categoria categoria){
        
        if(id != categoria.CategoriaId){
            return NotFound("Categoria não encontrada!");
        }

        _context.Categorias.Entry(categoria).State = EntityState.Modified;
        _context.SaveChanges();

        return Ok(categoria);
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id){

        var categoria = _context.Categorias.FirstOrDefault(c => c.CategoriaId == id);

        if(categoria is null){
            return NotFound("Categoria não econtrada!");
        }

        _context.Categorias.Remove(categoria);
        _context.SaveChanges();

        return Ok(categoria);

    }
}