namespace Ecommerce.API.DTOs
{
    public class AvaliacaoDTO
    {
        public long UsuarioId { get; set; }
        public long ProdutoId { get; set; }
        public int Nota { get; set; }
        public string Comentario { get; set; }
    }
}
