using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PIA_BackEnd.DTOs;
using PIA_BackEnd.Entidades;
using System.Globalization;

namespace PIA_BackEnd.Controllers
{
    [ApiController]
    [Route("api/eventos")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
    public class FormularioController : ControllerBase
    {
        private readonly ApplicationDBContext dbContext;
        public FormularioController(ApplicationDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost("/Hacer pregunta")]
        //[AllowAnonymous]
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

        [HttpGet("/LeerPregunta")]
        //[AllowAnonymous]
        public async Task<ActionResult> GetHacerPregunta(int id_organizador)
        {
            var exist = await dbContext.Usuario.AnyAsync(u => u.Id == id_organizador);
            if (!exist)
            {
                return BadRequest("Usuario No Encontrado");
            }
            else
            {
                var formularios = await dbContext.Formulario
                .Where(e => e.IdOrganizador.Equals(id_organizador))
                .ToListAsync();
                return Ok(formularios);
            }
        }
    }
}
