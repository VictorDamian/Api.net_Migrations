using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiVentas.Models
{
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [Column(TypeName =("VARCHAR(20)"))]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        [Column(TypeName =("VARCHAR(30)"))]
        public string Email { get; set; }
        [Required]
        [StringLength(maximumLength:100, MinimumLength =3)]
        public string Password { get; set; }
    }
}