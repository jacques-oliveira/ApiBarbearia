using System.Text.Json;
using System.Text.Json.Serialization;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("[controller]")]
public class ProdutosController : ControllerBase{
    private readonly IUnityOfWork _uow;
    private readonly IMapper _mapper;
    public ProdutosController(IUnityOfWork context, IMapper mapper)
    {
        _uow = context;
        _mapper = mapper;
    }

    [HttpGet("menorpreco")]
    public async Task<ActionResult<IEnumerable<ProdutoDTO>>> GetProdutosPrecos(){

        var produtos = await _uow.ProdutoRepository.GetProdutosPorPreco();
        var produtosDto = _mapper.Map<List<ProdutoDTO>>(produtos);

        return produtosDto;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProdutoDTO>>> Get([FromQuery] ProdutosParameters produtosParameters){

        var produtos = await _uow.ProdutoRepository.GetProdutos(produtosParameters);

        var metadata = new{
            produtos.TotalCount,
            produtos.PageSize,
            produtos.CurrentPage,
            produtos.TotalPages,
            produtos.HasNext,
            produtos.HasPrevious
        };

        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metadata));

        if(produtos is null){
            return NotFound("Produtos não econtrados!");
        }
        var produtosDto = _mapper.Map<List<ProdutoDTO>>(produtos);
        return produtosDto;
    }

    [HttpGet("{id:int}",Name ="ObterProduto")]
    public async Task<ActionResult<ProdutoDTO>> Get(int id){

        var produto = await _uow.ProdutoRepository.GetById(p => p.ProdutoId == id);

        if(produto is null){
            return NotFound("Produto não encontrado");
        }
        var produtoDto = _mapper.Map<ProdutoDTO>(produto);

        return produtoDto;
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody]ProdutoDTO produtoDto){

        if(produtoDto is null){
            return BadRequest();
        }

        var produto = _mapper.Map<Produto>(produtoDto);

        _uow.ProdutoRepository.Add(produto);
        await _uow.Commit();

        var produtoDTO = _mapper.Map<ProdutoDTO>(produto);

        return new CreatedAtRouteResult("ObterProduto",
            new {id = produto.ProdutoId},produtoDTO);

    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Put(int id, [FromBody] ProdutoDTO produtoDto){

        if(id != produtoDto.ProdutoId){
            return BadRequest();
        }

        var produto = _mapper.Map<Produto>(produtoDto);

        _uow.ProdutoRepository.Update(produto);
        await _uow.Commit();

        return Ok(produto);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<ProdutoDTO>> Delete(int id){
        
        var produto = await _uow.ProdutoRepository.GetById(p => p.ProdutoId == id);

        if(produto is null){
            return NotFound("O produto não existe!");
        }

        _uow.ProdutoRepository.Delete(produto);
        await _uow.Commit();

        var produtoDto = _mapper.Map<ProdutoDTO>(produto);

        return Ok(produtoDto);
    }

}