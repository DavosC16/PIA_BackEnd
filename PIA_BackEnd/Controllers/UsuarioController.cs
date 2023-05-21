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
    public class UsuarioController : ControllerBase
    {
        private readonly ApplicationDBContext dbContext;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;

        public UsuarioController(ApplicationDBContext dbContext)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.configuration = configuration;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<GetUsuarioDTO>>> Get()
        {
            var alumnos = await dbContext.Usuario.ToListAsync();
            return mapper.Map<List<GetUsuarioDTO>>(alumnos);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UsuarioDTO usuarioDto)
        {
            //Ejemplo para validar desde el controlador con la BD con ayuda del dbContext

            var existeUsuarioMismoNombre = await dbContext.Usuario.AnyAsync(x => x.Nombre == usuarioDto.Nombre);

            if (existeUsuarioMismoNombre)
            {
                return BadRequest($"Ya existe un usuario con el nombre {usuarioDto.Nombre}");
            }

            var usuario = mapper.Map<Usuario>(usuarioDto);

            dbContext.Add(usuario);
            await dbContext.SaveChangesAsync();

            var usuarioDTO = mapper.Map<GetUsuarioDTO>(usuario);

            return CreatedAtRoute("obteneralumno", new { id = usuario.Id }, usuarioDTO);
        }
    }
}