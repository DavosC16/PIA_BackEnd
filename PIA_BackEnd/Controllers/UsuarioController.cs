using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PIA_BackEnd.DTOs;

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
    }
}