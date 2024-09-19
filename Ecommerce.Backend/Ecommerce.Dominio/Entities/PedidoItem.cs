using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Dominio.Entities
{
    public class PedidoItem
    {
        [Key]
        public long Id { get; set; }
        public Pedido Pedido { get; set; }
        public Produto Produto { get; set; }
        [Required]
        public int Quantidade { get; set; }
        public decimal Preco { get; set; }
    }
}
