public interface IUsuarioRepository :IRepository<Usuario>{
    Task<PagedList<Usuario>> GetUsuarios(UsuariosParameters usuariosParameters);
}