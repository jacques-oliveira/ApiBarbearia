public class UnityOfWork : IUnityOfWork{
    private ProdutoRepository _produtoRepo;
    private CategoriaRepository _categoriaRepo;

    private UsuarioRepository _usuarioRepo;
    public AppDbContext _context;

    public IProdutoRepository ProdutoRepository {
        get{
            return _produtoRepo = _produtoRepo ?? new ProdutoRepository(_context);
        }
    }

    public ICategoriaRepository CategoriaRepository {
        get {
            return _categoriaRepo = _categoriaRepo ?? new CategoriaRepository(_context);
        }
    }

    public IUsuarioRepository UsuarioRepository{
        get{
            return _usuarioRepo = _usuarioRepo ?? new UsuarioRepository(_context);
        }
    }
    public UnityOfWork(AppDbContext context)
    {
        _context = context;
    }

    public void Commit()
    {
        _context.SaveChanges();
    }

    public void Dispose(){
        _context.Dispose();
    }
}