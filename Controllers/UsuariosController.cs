using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("[controller]")]
public class UsuariosController : ControllerBase{
    private readonly IUnityOfWork _uow;

    public UsuariosController(IUnityOfWork context)
    {
        _uow = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Usuario>> Get(){
        var usuarios = _uow.UsuarioRepository.Get().ToList();

        if(usuarios is null){
            return BadRequest("Usuários não encontrados");
        }

        return usuarios;
    }

    [HttpGet("{id:int}",Name ="ObterUsuario")]
    public ActionResult<Usuario> Get(int id){
        var usuario = _uow.UsuarioRepository.GetById(u => u.UsuarioId == id);

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

        _uow.UsuarioRepository.Add(usuario);
        _uow.Commit();

        return new CreatedAtRouteResult("ObterUsuario",
            new { id = usuario.UsuarioId, usuario});
    }

    [HttpPut("{id:int}")]
    public ActionResult<Usuario> Put(int id, Usuario usuario){
        
        if(id != usuario.UsuarioId){
            return NotFound("O usuário não existe!");
        }

        _uow.UsuarioRepository.Update(usuario);
        _uow.Commit();

        return Ok(usuario);
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id){
        var usuario = _uow.UsuarioRepository.GetById(u => u.UsuarioId == id);

        if(usuario is null){
            return BadRequest("Usuario não encontrado!");
        }

        _uow.UsuarioRepository.Delete(usuario);
        _uow.Commit();

        return Ok(usuario);
    }
}