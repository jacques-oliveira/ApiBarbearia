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
}