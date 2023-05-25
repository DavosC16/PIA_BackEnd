//using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using PIA_BackEnd.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace PIA_BackEnd.Controllers
{
    [Route("api/usuarioregistrado")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]

    public class AsistenciaRegistroController : ControllerBase
    {
        private readonly ApplicationDBContext dbContext;
        public AsistenciaRegistroController(ApplicationDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost("/Ingresar Informacion del Registro")]
        //[AllowAnonymous]
        public async Task<ActionResult> CrearAsistencia(int id_us,int id_ev)
        {
            var existingEvent = await dbContext.Eventos.FindAsync(id_ev);
            if (existingEvent == null)
            {
                return NotFound();
            }

            if (existingEvent.MaxCapacidad > 0)
            {
                existingEvent.MaxCapacidad = (existingEvent.MaxCapacidad) - 1;
            }
            
            var createdEvent = new AsistenciaRegistrada
            {
                IdUsuario = id_us,
                IdEvento = id_ev

            };

            dbContext.UsuarioRegistro.Add(createdEvent);
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
