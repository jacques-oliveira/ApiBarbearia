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
    public ActionResult<IEnumerable<ProdutoDTO>> GetProdutosPrecos(){

        var produtos = _uow.ProdutoRepository.GetProdutosPorPreco().ToList();
        var produtosDto = _mapper.Map<List<ProdutoDTO>>(produtos);

        return produtosDto;
    }

    [HttpGet]
    public ActionResult<IEnumerable<ProdutoDTO>> Get(){

        var produtos = _uow.ProdutoRepository.Get().ToList();

        if(produtos is null){
            return NotFound("Produtos não econtrados!");
        }
        var produtosDto = _mapper.Map<List<ProdutoDTO>>(produtos);
        return produtosDto;
    }

    [HttpGet("{id:int}",Name ="ObterProduto")]
    public ActionResult<ProdutoDTO> Get(int id){

        var produto = _uow.ProdutoRepository.GetById(p => p.ProdutoId == id);

        if(produto is null){
            return NotFound("Produto não encontrado");
        }
        var produtoDto = _mapper.Map<ProdutoDTO>(produto);
        return produtoDto;
    }

    [HttpPost]
    public ActionResult Post([FromBody]ProdutoDTO produtoDto){

        if(produtoDto is null){
            return BadRequest();
        }

        var produto = _mapper.Map<Produto>(produtoDto);

        _uow.ProdutoRepository.Add(produto);
        _uow.Commit();

        var produtoDTO = _mapper.Map<ProdutoDTO>(produto);

        return new CreatedAtRouteResult("ObterProduto",
            new {id = produto.ProdutoId},produtoDTO);

    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id, [FromBody] ProdutoDTO produtoDto){

        if(id != produtoDto.ProdutoId){
            return BadRequest();
        }

        var produto = _mapper.Map<Produto>(produtoDto);

        _uow.ProdutoRepository.Update(produto);
        _uow.Commit();

        return Ok(produto);
    }

    [HttpDelete("{id:int}")]
    public ActionResult<ProdutoDTO> Delete(int id){
        
        var produto = _uow.ProdutoRepository.GetById(p => p.ProdutoId == id);

        if(produto is null){
            return NotFound("O produto não existe!");
        }

        _uow.ProdutoRepository.Delete(produto);
        _uow.Commit();

        var produtoDto = _mapper.Map<ProdutoDTO>(produto);

        return Ok(produtoDto);
    }

}