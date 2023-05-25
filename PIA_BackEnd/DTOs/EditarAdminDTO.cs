using System.ComponentModel.DataAnnotations;

namespace PIA_BackEnd.DTOs
{
    public class EditarAdminDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}