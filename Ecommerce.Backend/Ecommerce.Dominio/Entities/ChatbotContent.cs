using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Dominio.Entities
{
    public class ChatbotContent
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string Role { get; set; }
        [Required]
        public string Content { get; set; }
    }
}
