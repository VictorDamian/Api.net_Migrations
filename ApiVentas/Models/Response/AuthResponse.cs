using System.ComponentModel.DataAnnotations;

namespace ApiVentas.Models.Response
{
    public class AuthResponse
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}