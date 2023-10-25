public interface ICategoriaRepository : IRepository<Categoria>{
    IEnumerable<Categoria> GetCategoriasProdutos();
}