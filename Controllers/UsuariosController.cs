using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("[controller]")]
public class UsuariosController : ControllerBase{
    private readonly IUnityOfWork _uow;
    private readonly IMapper _mapper;
    public UsuariosController(IUnityOfWork context, IMapper mapper)
    {
        _uow = context;
        _mapper = mapper;
    }

    [HttpGet]
    public ActionResult<IEnumerable<UsuarioDTO>> Get(){
        var usuarios = _uow.UsuarioRepository.Get().ToList();

        if(usuarios is null){
            return BadRequest("Usuários não encontrados");
        }

        var usuariosDto = _mapper.Map<List<UsuarioDTO>>(usuarios);
        return usuariosDto;
    }

    [HttpGet("{id:int}",Name ="ObterUsuario")]
    public ActionResult<UsuarioDTO> Get(int id){
        var usuario = _uow.UsuarioRepository.GetById(u => u.UsuarioId == id);

        if(usuario is null){
            return NotFound("Usuario não encontrado!");
        }

        var usuarioDto = _mapper.Map<UsuarioDTO>(usuario);
        return usuarioDto;
    }

    [HttpPost]
    public ActionResult Post([FromBody]Usuario usuario){
        if(usuario is null){
            return BadRequest();
        }

        _uow.UsuarioRepository.Add(usuario);
        _uow.Commit();

        var usuarioDto = _mapper.Map<UsuarioDTO>(usuario);

        return new CreatedAtRouteResult("ObterUsuario",
            new { id = usuarioDto.UsuarioId, usuarioDto});
    }

    [HttpPut("{id:int}")]
    public ActionResult<UsuarioDTO> Put(int id, UsuarioDTO usuarioDto){
        
        if(id != usuarioDto.UsuarioId){
            return NotFound("O usuário não existe!");
        }

        var usuario = _mapper.Map<Usuario>(usuarioDto);

        _uow.UsuarioRepository.Update(usuario);
        _uow.Commit();

        return Ok(usuarioDto);
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id){
        var usuario = _uow.UsuarioRepository.GetById(u => u.UsuarioId == id);

        if(usuario is null){
            return BadRequest("Usuario não encontrado!");
        }

        _uow.UsuarioRepository.Delete(usuario);
        _uow.Commit();

        var usuarioDto = _mapper.Map<UsuarioDTO>(usuario);
        
        return Ok(usuarioDto);
    }
}