using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using PIA_BackEnd.Entidades;
using System.Diagnostics.Tracing;
using System.Runtime.CompilerServices;

namespace PIA_BackEnd.Controllers
{
    public class EventosPopultaresController: ControllerBase
    {
        private readonly ApplicationDBContext dbContext;
        public EventosPopultaresController(ApplicationDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet("/Eventos Populares")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Eventos>>> Get()
        {
            return await dbContext.Eventos
                .Join(dbContext.Favoritos, e=>e.Id, f=>f.IdEvento, (e, f) => new { Eventos = e, Favoritos = f })
                .GroupBy(x=>x.Favoritos.IdEvento)
                .OrderByDescending(x=>x.Count()).Take(3)
                .Select(x=>x.FirstOrDefault().Eventos)
                .ToListAsync(); 
        }
    }
}
