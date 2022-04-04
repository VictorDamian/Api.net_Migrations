using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiVentas.Models
{
    public class DetalleVenta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int VentaId { get; set; }
        public int ProductoId { get; set; }
        [Required]
        public int Cantidad { get; set; }
        [Required]
        [Column(TypeName ="Decimal(16,2)")]
        public decimal PrecioU { get; set; }
        [Required]
        [Column(TypeName ="Decimal(16,2)")]
        public decimal Importe { get; set; }
    }
}