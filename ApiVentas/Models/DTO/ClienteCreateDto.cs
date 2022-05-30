using System.ComponentModel.DataAnnotations;

namespace ApiVentas.Models.DTO
{
    public class ClienteCreateDto
    {
        [Required(ErrorMessage = "Nombre requerido")]
        public string Nombre { get; set; }
    }
}