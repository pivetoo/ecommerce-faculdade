using Ecommerce.Dominio.ValueObjects;

namespace Ecommerce.Dominio.Entities
{
    public class ProdutoRelacao
    {
        public long Id { get; set; }
        public long ProdutoId { get; set; }
        public long ProdutoRelacionadoId { get; set; }
        public TipoRelacao Tipo { get; set; }
        public Produto Produto { get; set; }
        public Produto ProdutoRelacionado { get; set; }
    }
}
