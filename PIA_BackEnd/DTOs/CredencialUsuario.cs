using System.ComponentModel.DataAnnotations;

namespace PIA_BackEnd.DTOs
{
    public class CredencialesUsuario
    {
        [Required]
        [EmailAddress]
        public string Mail { get; set; }
        [Required]
        public string Password { get; set; }
    }
}