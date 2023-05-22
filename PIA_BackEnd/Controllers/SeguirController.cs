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
    [Route("api/usuarioseguido")]

    public class SeguirController : ControllerBase
    {
        private readonly ApplicationDBContext dbContext;
        public SeguirController(ApplicationDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost("/Seguir organizador")]
        public async Task<ActionResult> SeguirOrganizador(int id_organizador, int id_usuario)
        {
            var usuarioexiste = await dbContext.Usuario.FindAsync(id_usuario);
            var organizadorexiste = await dbContext.Usuario.FindAsync(id_organizador);
            if (usuarioexiste == null || organizadorexiste == null)
            {
                return NotFound();
            }

            var seguidorCreado = new Seguidor
            {
                IdUsuario = id_usuario,
                IdOrganizador = id_organizador

            };

            dbContext.Seguidor.Add(seguidorCreado);
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
