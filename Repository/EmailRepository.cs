public class EmailRepository : Repository<Email> , IEmailRepository
{
    public EmailRepository(AppDbContext context) : base(context)
    {
    }
}