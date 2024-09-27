using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Dominio.Entities
{
    public class Usuario
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Senha { get; set; }
        public string? Telefone { get; set; }
        public string? Endereco { get; set; }
        public bool IsAdmin { get; set; }
        public DateTime? CriadoEm { get; set; }
    }
}
