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

    [HttpGet("{id:int}",Name ="ObterUsuario")]
    public ActionResult<Usuario> Get(int id){
        var usuario = _context.Usuarios.FirstOrDefault(u => u.UsuarioId == id);

        if(usuario is null){
            return NotFound("Usuario não encontrado!");
        }

        return usuario;
    }

    [HttpPost]
    public ActionResult Post(Usuario usuario){
        if(usuario is null){
            return BadRequest();
        }

        _context.Usuarios.Add(usuario);
        _context.SaveChanges();

        return new CreatedAtRouteResult("ObterUsuario",
            new { id = usuario.UsuarioId, usuario});
    }

    [HttpPut("{id:int}")]
    public ActionResult<Usuario> Put(int id, Usuario usuario){
        
        if(id != usuario.UsuarioId){
            return NotFound("O usuário não existe!");
        }

        _context.Entry(usuario).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        _context.SaveChanges();

        return Ok(usuario);
    }


}