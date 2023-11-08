public interface IUnityOfWork{
    IProdutoRepository ProdutoRepository {get;}
    ICategoriaRepository CategoriaRepository {get;}

    void Commit();
}