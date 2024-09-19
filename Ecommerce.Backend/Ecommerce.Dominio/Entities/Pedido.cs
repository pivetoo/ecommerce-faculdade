using Ecommerce.Dominio.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Dominio.Entities
{
    public class Pedido
    {
        [Key]
        public long Id { get; set; }
        public Usuario Usuario { get; set; }
        public decimal Total { get; set; }
        public StatusPedido Status { get; set; }
        public DateTime DataPedido { get; set; }
        public IList<Produto> Produtos { get; set; } = new List<Produto>();
    }
}
