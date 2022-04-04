using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiVentas.Models
{
    public class Venta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", 
        ApplyFormatInEditMode = true)]
        public string Fecha { get; set; }
        [Required]
        public int ClienteId { get; set; }
        [Required]
        [Column(TypeName ="Decimal(16,2)")]
        public decimal Total { get; set; }

        public virtual ICollection<DetalleVenta> DetalleVentas {get;set;}
    }
}