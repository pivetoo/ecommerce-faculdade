namespace Ecommerce.Application.Services
{
    public class PagamentoService
    {
        public string ProcessarPagamento(decimal valor)
        {
            var random = new Random();
            var resultado = random.Next(0, 100);

            if (resultado < 90)
            {
                return "Sucesso";
            }
            else
            {
                return "Falha";
            }
        }
    }
}
