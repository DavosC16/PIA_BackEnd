using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PIA_BackEnd.Entidades;

namespace PIA_BackEnd.Controllers
{
    [ApiController]
    [Route("api/eventos")]
    public class EventosController: ControllerBase
    {
        private readonly ApplicationDBContext dbContext;
        public EventosController(ApplicationDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet ("/Lista de Eventos")]
        public async Task<ActionResult<List<Eventos>>> Get()
        {
            return await dbContext.Eventos.ToListAsync();
        }

        [HttpPost ("/Nuevo Evento")]
        public async Task<ActionResult> Post(Eventos eventos)
        {
            dbContext.Add(eventos);
            await dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("/Editar Informacion del Evento")]
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

        [HttpDelete("/Cancelar Evento")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.Eventos.AnyAsync(x => x.Id == id);

            if (!exist)
            {
                return NotFound();
            }

            dbContext.Remove(new Eventos()
            {
                Id = id
            });

            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("/Busqueda de Evento")]
        public async Task<ActionResult<List<Eventos>>> Get(string busqueda)
        {
            var exist = await dbContext.Eventos.AnyAsync(e => e.Nombre.Contains(busqueda)|| e.Fecha == busqueda || e.Ubicacion.Contains(busqueda));

            if (!exist)
            {
                return BadRequest("Evento No Encontrado");
            }
            else
            {
                return await dbContext.Eventos.Where(e => e.Nombre.Contains(busqueda) || e.Fecha == busqueda || e.Ubicacion.Contains(busqueda)).ToListAsync();
            }
        }

        [HttpGet("/Asistentes")]
        public async Task<ActionResult<List<Usuario>>> Get(int id)
        {
            var exist = await dbContext.Eventos.AnyAsync(e => e.Id == id);
            if (!exist)
            {
                return BadRequest("Evento No Encontrado");
            }
            else
            {
                var listaAsistentes = from user in dbContext.Usuario
                                      join usreg in dbContext.UsuarioRegistro
                                      on user.Id equals usreg.IdUsuario
                                      where usreg.IdEvento == id
                                      select user;
                return Ok(listaAsistentes);
            }
        }
    }
}
