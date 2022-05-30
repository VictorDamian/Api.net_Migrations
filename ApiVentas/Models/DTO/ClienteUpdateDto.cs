using System.ComponentModel.DataAnnotations;

namespace ApiVentas.Models.DTO
{
    public class ClienteUpdateDto
    {
        [Required(ErrorMessage = "Nombre requerido")]
        public string Nombre { get; set; }
    }
}