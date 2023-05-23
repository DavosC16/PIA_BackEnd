using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PIA_BackEnd.Entidades
{
    public class Favoritos
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int IdUsuario { get; set; }

        public int IdEvento { get; set; }
    }
}
