
using Microsoft.EntityFrameworkCore;

public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
{
    public CategoriaRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<PagedList<Categoria>> GetCategorias(CategoriasParameters categoriaParameters)
    {
        return await PagedList<Categoria>.ToPagedList(Get().OrderBy(on => on.CategoriaId),
                    categoriaParameters.PageNumber, categoriaParameters.PageSize);
    }

    public async Task<IEnumerable<Categoria>> GetCategoriasProdutos()
    {
        return await Get().Include( p => p.Produtos).ToListAsync();
    }    
}