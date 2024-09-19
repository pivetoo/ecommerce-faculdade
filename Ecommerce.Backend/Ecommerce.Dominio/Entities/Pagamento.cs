using Ecommerce.Dominio.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Dominio.Entities
{
    public class Pagamento
    {
        [Key]
        public long Id { get; set; }
        public Pedido Pedido { get; set; }
        [Required]
        public MetodoPagamento MetodoPagamento { get; set; }
        public decimal Valor { get; set; }
        public StatusPagamento StatusPagamento { get; set; }
        public DateTime? DataPagamento { get; set; }
    }
}
