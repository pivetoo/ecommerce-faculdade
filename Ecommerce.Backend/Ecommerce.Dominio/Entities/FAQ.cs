using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Dominio.Entities
{
    public class FAQ
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string Titulo { get; set; }
        [Required]
        public string Descricao { get; set; }
        [Required]
        [StringLength(1000)]
        public string? ImagemUrl { get; set; }
        [Required]
        public string SaibaMais { get; set; }
        public DateTime CriadoEm { get; set; }
    }
}
