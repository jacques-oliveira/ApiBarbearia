
using Microsoft.EntityFrameworkCore;

public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
{
    public CategoriaRepository(AppDbContext context) : base(context)
    {
    }

    public IEnumerable<Categoria> GetCategoriasProdutos()
    {
        return Get().Include( p => p.Produtos);
    }
}