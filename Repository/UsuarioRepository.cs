
using Microsoft.EntityFrameworkCore;

public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
{
    public UsuarioRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Usuario>> GetDadosUsuarios()
    {
        return await Get()
                    .Include(e=> e.Endereco)
                    .Include(em=> em.Email)
                    .Where( u=> u.Endereco != null || u.Email != null)
                    .ToListAsync();
    }

    public async Task<PagedList<Usuario>> GetUsuarios(UsuariosParameters usuariosParameters)
    {
        return await PagedList<Usuario>.ToPagedList(Get().OrderBy(u=> u.Nome),
        usuariosParameters.PageNumber, usuariosParameters.PageSize);
    }
}