using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("[controller]")]
public class CategoriasController : ControllerBase{
    private readonly IUnityOfWork _uow;
    private readonly IMapper _mapper;

    public CategoriasController(IUnityOfWork context, IMapper mapper)
    {
        _uow = context;
        _mapper = mapper;
    }

    [HttpGet]
    public ActionResult<IEnumerable<CategoriaDTO>> Get(){

        var categorias = _uow.CategoriaRepository.Get().ToList();
        try{

            if(categorias is null){

                return BadRequest("Categoria Nâo econtrada!");
            }

        }catch(Exception){

            return StatusCode(StatusCodes.Status500InternalServerError,
            "Ocorreu um problema ao tratar sua solicitação.");
        }
        var categoriasDto = _mapper.Map<List<CategoriaDTO>>(categorias);                

        return categoriasDto;
    }

    [HttpGet("produtos")]
    public ActionResult<IEnumerable<CategoriaDTO>> GetCategoriaProduto(){
        var categoriaProdutos = _uow.CategoriaRepository.GetCategoriasProdutos().ToList();
        try{

            if(categoriaProdutos is null){
                return BadRequest("Categoria/Produtos não encontrada");
            }

        }catch(Exception){

            return StatusCode(StatusCodes.Status500InternalServerError,
            "Ocorreu um problema ao tratar sua solicitação.");
        }
        var categoriaProdutosDto = _mapper.Map<List<CategoriaDTO>>(categoriaProdutos);
        return categoriaProdutosDto;        
    }

    [HttpGet("{id:int}")]
    public ActionResult<CategoriaDTO> Get(int id){

        try{
            var categoria = _uow.CategoriaRepository.GetById(c => c.CategoriaId == id);

            if(categoria is null){
                return NotFound("Categoria não encontrada");
            }
            var categoriaDto = _mapper.Map<CategoriaDTO>(categoria);

            return Ok(categoriaDto);

        }catch(Exception){
            return StatusCode(StatusCodes.Status500InternalServerError,
            "Ocorreu um problema ao tratar sua solicitação.");
        }

    }

    [HttpPost]
    public ActionResult Post(CategoriaDTO categoriaDto){

        if(categoriaDto is null){
            return BadRequest();
        }

        var categoria = _mapper.Map<Categoria>(categoriaDto);
        _uow.CategoriaRepository.Add(categoria);
        _uow.Commit();

        return new CreatedAtRouteResult("ObterCategoria",
            new {id = categoria.CategoriaId, categoria});
    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id, CategoriaDTO categoriaDto){
        
        if(id != categoriaDto.CategoriaId){
            return NotFound("Categoria não encontrada!");
        }
        var categoria = _mapper.Map<Categoria>(categoriaDto);
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

        var categoriaDto = _mapper.Map<CategoriaDTO>(categoria);

        return Ok(categoriaDto);

    }
}