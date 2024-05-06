using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Agendamento> Agendamentos {get;set;}
    public DbSet<Categoria> Categorias {get; set;}
    public DbSet<Email> Emails {get;set;}
    public DbSet<Endereco> Enderecos {get;set;}
    public DbSet<Produto> Produtos {get; set;}
    public DbSet<Telefone> Telefones {get; set;}
    public DbSet<Usuario> Usuarios {get;set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}