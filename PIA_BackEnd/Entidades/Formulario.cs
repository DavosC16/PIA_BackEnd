using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PIA_BackEnd.Entidades
{
    public class Formulario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int IdOrganizador { get; set; }
        
        public int IdUsuario { get; set; }

        public string Mensaje { get; set; }

    }
}
