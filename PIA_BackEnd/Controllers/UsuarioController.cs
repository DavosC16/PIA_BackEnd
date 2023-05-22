using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PIA_BackEnd.DTOs;
using PIA_BackEnd.Entidades;

namespace PIA_BackEnd.Controllers
{
    [ApiController]
    [Route("usuario")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsUsuario")]
    public class UsuarioController : ControllerBase
    {
        private readonly ApplicationDBContext dbContext;
        

        public UsuarioController(ApplicationDBContext dbContext)
        {
            this.dbContext = dbContext;
           
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<GetUsuarioDTO>>> Get()
        {
            var usuarios = await dbContext.Usuario.ToListAsync();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Usuario, GetUsuarioDTO>();
            });

            var mapper = new Mapper(config);
            var usuariosDTO = mapper.Map<List<GetUsuarioDTO>>(usuarios);

            return usuariosDTO;
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Post([FromBody] UsuarioDTO usuarioDto)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UsuarioDTO, Usuario>();
                cfg.CreateMap<Usuario, GetUsuarioDTO>();
            });

            var mapper = new Mapper(config);
            var existeUsuarioMismoNombre = await dbContext.Usuario.AnyAsync(x => x.Nombre == usuarioDto.Nombre);

            if (existeUsuarioMismoNombre)
            {
                return BadRequest($"Ya existe un usuario con el nombre {usuarioDto.Nombre}");
            }

            var usuario = mapper.Map<Usuario>(usuarioDto);

            dbContext.Add(usuario);
            await dbContext.SaveChangesAsync();

            var usuarioDTO = mapper.Map<GetUsuarioDTO>(usuario);

            return CreatedAtRoute("obtenerusuario", new { id = usuario.Id }, usuarioDTO);
        }

        [HttpGet("/Eventos Registrados")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Eventos>>> Get(int id)
        {
            var exist = await dbContext.Usuario.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return BadRequest("Usuario Inexistente");
            }
            else
            {
                var listaEventos = from eventos in dbContext.Eventos
                                   join usreg in dbContext.UsuarioRegistro
                                   on eventos.Id equals usreg.IdEvento
                                   where usreg.IdUsuario == id
                                   select eventos;
                return Ok(listaEventos);
            }
        }
    }
}