using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Dominio.Entities
{
    public class Categoria
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string? Nome { get; set; }
        [Required]
        public IList<Produto> Produtos { get; set; } = new List<Produto>();
    }
}
