using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Dominio.Entities
{
    public class Produto
    {
        [Key]
        public long Id { get; set; }
        public long CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Descricao { get; set; }
        [Required]
        public decimal Preco { get; set; }
        [StringLength(1000)]
        public string? ImagemUrl { get; set; }
        public int Quantidade { get; set; }
        [Required]
        public int Estoque { get; set; }
        public DateTime? CriadoEm { get; set; }
        public double NotaMedia { get; set; }
        public IList<Avaliacao> Avaliacoes { get; set; }
    }
}
