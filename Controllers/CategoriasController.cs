using System.Text.Json;
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
    public async Task<ActionResult<IEnumerable<CategoriaDTO>>> Get([FromQuery] CategoriasParameters categoriaParameters){

        var categorias = await _uow.CategoriaRepository.GetCategorias(categoriaParameters);

        var metadata = new {
            categorias.TotalCount,
            categorias.PageSize,
            categorias.CurrentPage,
            categorias.TotalPages,
            categorias.HasNext,
            categorias.HasPrevious
        };

        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metadata));
        
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
    public async Task<ActionResult<IEnumerable<CategoriaDTO>>> GetCategoriaProduto(){

        var categoriaProdutos = await _uow.CategoriaRepository
                                .GetCategoriasProdutos();
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
    public async Task<ActionResult<CategoriaDTO>> Get(int id){

        try{
            var categoria = await _uow.CategoriaRepository
                            .GetById(c => c.CategoriaId == id);

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
    public async Task<ActionResult> Post(CategoriaDTO categoriaDto){

        if(categoriaDto is null){
            return BadRequest();
        }

        var categoria = _mapper.Map<Categoria>(categoriaDto);
        _uow.CategoriaRepository.Add(categoria);
        await _uow.Commit();

        return new CreatedAtRouteResult("ObterCategoria",
            new {id = categoria.CategoriaId, categoria});
    }

    [HttpPut("{id:int}")]
    public async  Task<ActionResult> Put(int id, CategoriaDTO categoriaDto){
        
        if(id != categoriaDto.CategoriaId){
            return NotFound("Categoria não encontrada!");
        }
        var categoria = _mapper.Map<Categoria>(categoriaDto);
        _uow.CategoriaRepository.Update(categoria);
        await _uow.Commit();

        return Ok(categoria);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id){

        var categoria = await _uow.CategoriaRepository.GetById(c => c.CategoriaId == id);

        if(categoria is null){
            return NotFound("Categoria não econtrada!");
        }

        _uow.CategoriaRepository.Delete(categoria);
        await _uow.Commit();

        var categoriaDto = _mapper.Map<CategoriaDTO>(categoria);

        return Ok(categoriaDto);

    }
}