using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Application.DTOs
{
    public class ProdutoDTO
    {
        [Required]
        public string Nome { get; set; }

        [Required]
        public string Descricao { get; set; }

        [Required]
        public decimal Preco { get; set; }

        public string? ImagemUrl { get; set; }

        public int Quantidade { get; set; }

        [Required]
        public int Estoque { get; set; }

        [Required]
        public long CategoriaId { get; set; }
    }
}
