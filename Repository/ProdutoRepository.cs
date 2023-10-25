
public class ProdutoRepository : Repository<Produto>, IProdutoRepository
{
    public ProdutoRepository(AppDbContext context) : base(context)
    {
    }

    public IEnumerable<Produto> GetProdutosPorPreco()
    {
        return Get().OrderBy(p=> p.Preco).ToList();
    }
}