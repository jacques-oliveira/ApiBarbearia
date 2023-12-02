
using Microsoft.EntityFrameworkCore;

public class AgendamentoRepository : Repository<Agendamento>, IAgendamentoRepository
{
    public AgendamentoRepository(AppDbContext context) : base(context)
    {
    }

    public IEnumerable<Agendamento> GetAgendamentoUsuario()
    {
        return Get().Include(u=> u.Usuarios)
            .Include(a => a.Produtos).AsNoTracking().ToList();
    }
}