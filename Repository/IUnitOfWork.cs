public interface IUnityOfWork{
    IProdutoRepository ProdutoRepository {get;}
    ICategoriaRepository CategoriaRepository {get;}

    IUsuarioRepository UsuarioRepository {get;}
    void Commit();
}