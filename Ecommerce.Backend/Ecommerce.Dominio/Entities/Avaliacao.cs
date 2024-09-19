using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Dominio.Entities
{
    public class Avaliacao
    {
        [Key]
        public long Id { get; set; }
        public Usuario Usuario { get; set; }
        public Produto Produto { get; set; }
        [Required]
        public int Nota { get; set; }
        [Required]
        public string Comentario { get; set; }
        public DateTime? DataAvaliacao { get; set; }
    }
}
