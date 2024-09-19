using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Dominio.Entities
{
    public class Produto
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Descricao { get; set; }
        [Required]
        public decimal Preco { get; set; }
        [Required]
        public int Quantidade { get; set; }
        [Required]
        public int Estoque { get; set; }
        [Required]
        public Categoria Categoria { get; set; }
        public DateTime? CriadoEm { get; set; }
        public double NotaMedia { get; set; }
        public IList<Avaliacao> Avaliacoes { get; set; }
    }
}
