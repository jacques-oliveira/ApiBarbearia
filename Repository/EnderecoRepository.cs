public class EnderecoRepository : Repository<Endereco>, IEnderecoRepository{
    public EnderecoRepository(AppDbContext context) : base(context){

    }
}