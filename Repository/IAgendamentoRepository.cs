public interface IAgendamentoRepository : IRepository<Agendamento>{
    Task<IEnumerable<Agendamento>> GetAgendamentoUsuario();
}