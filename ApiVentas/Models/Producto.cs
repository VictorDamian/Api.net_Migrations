using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiVentas.Models
{
    public class Producto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [Column(TypeName =("VARCHAR(50)"))]
        public string Nombre { get; set; }
        [Required]
        [Column(TypeName =("DECIMAL(16,2)"))]
        public decimal PrecioU { get; set; }
        [Required]
        [Column(TypeName =("DECIMAL(16,0)"))]
        public decimal Costo { get; set; }

        public virtual ICollection<DetalleVenta> DetalleVentas {get;set;}
    }
}