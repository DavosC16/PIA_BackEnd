using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PIA_BackEnd.Entidades;
using System.Globalization;
using PIA_BackEnd.DTOs;

namespace PIA_BackEnd.Controllers
{
    [ApiController]
    [Route("api/notificaciones")]
    public class NotificacionesController : ControllerBase
    {
        private readonly ApplicationDBContext dbContext;
        public NotificacionesController(ApplicationDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet("/MisNotificaciones")]

        //ingresa id usuario, trae fechas eventos, si evento en menos de 1 mes -> trae notificacion

        public async Task<ActionResult<List<EventoDTO>>> Get(int id_usuario)
        {
            var exist = await dbContext.Usuario.AnyAsync(u => u.Id == id_usuario);
            if (!exist)
            {
                return BadRequest("Usuario No Encontrado");
            }
            else
            {
                DateTime currentDate = DateTime.Now;
                TimeSpan dateRange = TimeSpan.FromDays(30);

                var listaEventos = dbContext.Eventos
                    .Join(
                        dbContext.UsuarioRegistro,
                        evento => evento.Id,
                        usreg => usreg.IdEvento,
                        (evento, usreg) => new { Evento = evento, UsuarioRegistro = usreg }
                    )
                    .Where(joinResult => joinResult.UsuarioRegistro.IdUsuario == id_usuario)
                    .AsEnumerable()
                    .Where(joinResult => DateTime.ParseExact(joinResult.Evento.Fecha, "dd-MM-yyyy", CultureInfo.InvariantCulture) >= currentDate - dateRange &&
                                         DateTime.ParseExact(joinResult.Evento.Fecha, "dd-MM-yyyy", CultureInfo.InvariantCulture) <= currentDate + dateRange)
                    .Select(joinResult => new EventoDTO
                    {
                        Notificacion = $"{joinResult.Evento.Nombre} es en {(DateTime.ParseExact(joinResult.Evento.Fecha, "dd-MM-yyyy", CultureInfo.InvariantCulture) - currentDate).Days} dias"
                    })
                    .ToList();

                return Ok(listaEventos);
            }
        }

    }
}
