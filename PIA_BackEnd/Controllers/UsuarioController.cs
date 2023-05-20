using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using PIA_BackEnd.Entidades;

namespace PIA_BackEnd.Controllers
{
    public class UsuarioController
    {
        [ApiController]
        [Route("api/usuario")]

        public class UsuarioController : ControllerBase
        {
            private readonly UserManager<IdentityUser> userManager;
            private readonly IConfiguration configuration;
            private readonly SignInManager<IdentityUser> signInManager;

            public UsuarioController(UserManager<IdentityUser> userManager, IConfiguration configuration,
            SignInManager<IdentityUser> signInManager)
            {
                this.userManager = userManager;
                this.configuration = configuration;
                this.signInManager = signInManager;
            }

            [HttpPost("/Nuevo Usuario")]

        }

    }
}
