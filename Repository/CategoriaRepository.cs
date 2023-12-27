
using Microsoft.EntityFrameworkCore;

public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
{
    public CategoriaRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Categoria>> GetCategoriasProdutos()
    {
        return await Get().Include( p => p.Produtos).ToListAsync();
    }    
}