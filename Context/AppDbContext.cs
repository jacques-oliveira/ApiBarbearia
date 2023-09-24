using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Agendamento> Agendamentos {get;set;}
    public DbSet<Categoria> Categorias {get; set;}
    public DbSet<Email> Emails {get;set;}
    public DbSet<Endereco> Enderecos {get;set;}
    public DbSet<Produto> Produtos {get; set;}
    public DbSet<Telefone> Telefones {get; set;}
    public DbSet<Usuario> Usuarios {get;set;}

    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     modelBuilder.Entity<Agendamento>().HasKey(sc =>
    //     new {sc.UsuarioId, sc.ProdutoId, sc.Data});
    // }
}