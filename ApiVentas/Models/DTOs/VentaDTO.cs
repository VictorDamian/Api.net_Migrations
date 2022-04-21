using System.ComponentModel.DataAnnotations;
using ApiVentas.DAO;
using ApiVentas.Repositories;

namespace ApiVentas.Models.DTOs
{
    public class VentaDTO
    {
        [Required]
        [Range(1, Double.MaxValue, ErrorMessage = "El id del cliente debe ser mayor a 0")]
        public int ClienteId { get; set; }
        [Required]
        [MaxLength(1, ErrorMessage ="Debe existir el detalle de la venta")]
        public List<DetalleVentaDto> DetalleVentas {get;set;}
        public VentaDTO()
        {
            this.DetalleVentas = new List<DetalleVentaDto>();
        }
        public class DetalleVentaDto
        {
            public int ProductoId { get; set; }
            public int Cantidad { get; set; }
            public decimal PrecioU { get; set; }
            public decimal Importe { get; set; }
        }
    }
}