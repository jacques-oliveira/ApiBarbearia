
using Microsoft.EntityFrameworkCore;

public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
{
    public UsuarioRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<PagedList<Usuario>> GetUsuarios(UsuariosParameters usuariosParameters)
    {
        return await PagedList<Usuario>.ToPagedList(Get().OrderBy(u=> u.Nome),
        usuariosParameters.PageNumber, usuariosParameters.PageSize);
    }
}