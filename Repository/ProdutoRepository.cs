
using Microsoft.EntityFrameworkCore;

public class ProdutoRepository : Repository<Produto>, IProdutoRepository
{
    public ProdutoRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<PagedList<Produto>> GetProdutos(ProdutosParameters produtosParameters)
    {
        // return Get()
        //     .OrderBy(on => on.Nome)
        //     .Skip((produtosParameters.PageNumber -1) * produtosParameters.PageSize)
        //     .Take(produtosParameters.PageSize)
        //     .ToList();
        return await PagedList<Produto>.ToPagedList(Get().OrderBy(on => on.ProdutoId),
            produtosParameters.PageNumber, produtosParameters.PageSize);
    }

    public async Task<IEnumerable<Produto>> GetProdutosPorPreco()
    {
        return await Get().OrderBy(p=> p.Preco).ToListAsync();
    }
}