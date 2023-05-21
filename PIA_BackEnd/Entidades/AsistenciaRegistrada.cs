using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace PIA_BackEnd.Entidades
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class AsistenciaRegistrada
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        public int IdUsuario { get; set; }

        public int IdEvento { get; set; }


    }
}
