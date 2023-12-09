public interface IAgendamentoRepository : IRepository<Agendamento>{
    public IEnumerable<Agendamento> GetAgendamentoUsuario();
}