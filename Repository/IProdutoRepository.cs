public interface IProdutoRepository : IRepository<Produto>{
    PagedList<Produto> GetProdutos(ProdutosParameters produtosParameters);
    IEnumerable<Produto> GetProdutosPorPreco();
}