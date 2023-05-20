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

namespace PIA_BackEnd.Controllers
{
    [ApiController]
    [Route("api/usuarioregistrado")]

    public class UsuarioRegController : ControllerBase
    {
        private readonly ApplicationDBContext dbContext;
        public UsuarioRegController(ApplicationDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPut("/Editar Informacion del Registro")]
        public async Task<ActionResult> Put(Eventos eventos, int id)
        {
            if (eventos.Id != id)
            {
                return BadRequest("Id de Evento Inexistente");
            }
            dbContext.Update(eventos);
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
