using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Dominio.Entities
{
    public class ItemCarrinho
    {
        [Key]
        public long Id { get; set; }
        public Produto Produto { get; set; }
        public Carrinho Carrinho { get; set; }
        [Required]
        public int Quantidade { get; set; }
        public decimal Subtotal => Produto.Preco * Quantidade;
    }
}
