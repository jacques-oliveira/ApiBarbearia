public interface IProdutoRepository : IRepository<Produto>{
    IEnumerable<Produto> GetProdutos(ProdutosParameters produtosParameters);
    IEnumerable<Produto> GetProdutosPorPreco();
}