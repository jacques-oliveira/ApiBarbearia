using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class UsuariosController : ControllerBase{
    private readonly AppDbContext _context;

    public UsuariosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Usuario>> Get(){
        var usuarios = _context.Usuarios.ToList();

        if(usuarios is null){
            return BadRequest("Usuários não encontrados");
        }

        return usuarios;
    }

    [HttpGet("{id:int}")]
    public ActionResult<Usuario> Get(int id){
        var usuario = _context.Usuarios.FirstOrDefault(u => u.UsuarioId == id);

        if(usuario is null){
            return NotFound("Usuario não encontrado!");
        }

        return usuario;
    }
}