using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Dominio.Entities
{
    public class Carrinho
    {
        [Key]
        public long Id { get; set; }
        public Usuario Usuario { get; set; }
        [Required]
        public IList<ItemCarrinho> Itens { get; set; }
        public decimal Total { get; set; }
    }
}
