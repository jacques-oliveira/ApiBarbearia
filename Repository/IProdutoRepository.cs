public interface IProdutoRepository : IRepository<Produto>{
    IEnumerable<Produto> GetProdutosPorPreco();
}