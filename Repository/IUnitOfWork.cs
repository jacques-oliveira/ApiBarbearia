public interface IUnityOfWork{
    IProdutoRepository ProdutoRepository {get;}
    ICategoriaRepository CategoriaRepository {get;}
    IUsuarioRepository UsuarioRepository {get;}
    IEmailRepository EmailRepository {get;}
    IEnderecoRepository EnderecoRepository {get;}
    IAgendamentoRepository AgendamentoRepository {get;}
    void Commit();
}