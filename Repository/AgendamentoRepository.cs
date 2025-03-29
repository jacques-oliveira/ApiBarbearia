
using Microsoft.EntityFrameworkCore;

public class AgendamentoRepository : Repository<Agendamento>, IAgendamentoRepository
{
    public AgendamentoRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Agendamento>> GetAgendamentoUsuario()
    {
        return await Get()
            .Include(u=> u.Usuarios)
            .Include(a => a.Produtos)
            .AsNoTracking()
            .ToListAsync();
    }
}