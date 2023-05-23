using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PIA_BackEnd.Entidades;

namespace PIA_BackEnd.Controllers
{
    [ApiController]
    [Route("api/eventos")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsUsuario")]
    public class FormularioController : ControllerBase
    {
        private readonly ApplicationDBContext dbContext;
        public FormularioController(ApplicationDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost("/Hacer pregunta")]
        public async Task<ActionResult> HacerPregunta(int id_organizador, int id_usuario, string pregunta)
        {
            var usuarioexiste = await dbContext.Usuario.FindAsync(id_usuario);
            var organizadorexiste = await dbContext.Usuario.FindAsync(id_organizador);
            if (usuarioexiste == null || organizadorexiste == null)
            {
                return NotFound();
            }

            var preguntaCreada = new Formulario
            {
                IdUsuario = id_usuario,
                IdOrganizador = id_organizador,
                Mensaje = pregunta

            };

            dbContext.Formulario.Add(preguntaCreada);
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
