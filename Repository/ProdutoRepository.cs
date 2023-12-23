
public class ProdutoRepository : Repository<Produto>, IProdutoRepository
{
    public ProdutoRepository(AppDbContext context) : base(context)
    {
    }

    public PagedList<Produto> GetProdutos(ProdutosParameters produtosParameters)
    {
        // return Get()
        //     .OrderBy(on => on.Nome)
        //     .Skip((produtosParameters.PageNumber -1) * produtosParameters.PageSize)
        //     .Take(produtosParameters.PageSize)
        //     .ToList();
        return PagedList<Produto>.ToPagedList(Get().OrderBy(on => on.ProdutoId),
            produtosParameters.PageNumber, produtosParameters.PageSize);
    }

    public IEnumerable<Produto> GetProdutosPorPreco()
    {
        return Get().OrderBy(p=> p.Preco).ToList();
    }
}