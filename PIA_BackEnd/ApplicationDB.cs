using Microsoft.EntityFrameworkCore;
using PIA_BackEnd.Entidades;

namespace PIA_BackEnd
{
    public class ApplicationDBContext: DbContext
    {
        public ApplicationDBContext(DbContextOptions options) : base(options) { }

        public DbSet<Eventos> Eventos { get; set; }

        public DbSet<Usuario> Usuario { get; set; }

        public DbSet<AsistenciaRegistrada> UsuarioRegistro { get; set; }
    }
}
