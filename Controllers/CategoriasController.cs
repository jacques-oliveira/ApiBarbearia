using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("[controller]")]
public class CategoriasController : ControllerBase{
    private readonly IUnityOfWork _uow;

    public CategoriasController(IUnityOfWork context)
    {
        _uow = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Categoria>> Get(){

        try{

            return _uow.CategoriaRepository.Get().ToList();

        }catch(Exception){

            return StatusCode(StatusCodes.Status500InternalServerError,
            "Ocorreu um problema ao tratar sua solicitação.");
        }
        
    }

    [HttpGet("produtos")]
    public ActionResult<IEnumerable<Categoria>> GetCategoriaProduto(){

        try{

            return _uow.CategoriaRepository.GetCategoriasProdutos().ToList();

        }catch(Exception){

            return StatusCode(StatusCodes.Status500InternalServerError,
            "Ocorreu um problema ao tratar sua solicitação.");
        }
        
    }

    [HttpGet("{id:int}")]
    public ActionResult<Categoria> Get(int id){

        try{
            var categoria = _uow.CategoriaRepository.GetById(c => c.CategoriaId == id);

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

        _uow.CategoriaRepository.Add(categoria);
        _uow.Commit();

        return new CreatedAtRouteResult("ObterCategoria",
            new {id = categoria.CategoriaId, categoria});
    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Categoria categoria){
        
        if(id != categoria.CategoriaId){
            return NotFound("Categoria não encontrada!");
        }

        _uow.CategoriaRepository.Update(categoria);
        _uow.Commit();

        return Ok(categoria);
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id){

        var categoria = _uow.CategoriaRepository.GetById(c => c.CategoriaId == id);

        if(categoria is null){
            return NotFound("Categoria não econtrada!");
        }

        _uow.CategoriaRepository.Delete(categoria);
        _uow.Commit();

        return Ok(categoria);

    }
}