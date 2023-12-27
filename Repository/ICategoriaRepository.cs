public interface ICategoriaRepository : IRepository<Categoria>{
    Task<IEnumerable<Categoria>> GetCategoriasProdutos();
}