public interface IUsuarioRepository :IRepository<Usuario>{
    Task<PagedList<Usuario>> GetUsuarios(UsuariosParameters usuariosParameters);
    Task<IEnumerable<Usuario>> GetDadosUsuarios();
}