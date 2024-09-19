using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Dominio.Entities
{
    public class EnderecoEntrega
    {
        [Key]
        public long Id { get; set; }
        public Usuario Usuario { get; set; }
        public string Logradouro { get; set; }
        [Required]
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        [Required]
        public string Cidade { get; set; }
        [Required]
        public string Estado { get; set; }
        [Required]
        public string Cep { get; set; }
        [Required]
        public string Pais { get; set; }
        public bool EnderecoPrincipal { get; set; }
    }
}
