using Ecommerce.Dominio.Entities;
using Ecommerce.Dominio.IRepository;
using Ecommerce.Dominio.IService;
using Ecommerce.Dominio.ValueObjects;

namespace Ecommerce.Application.Services
{
    public class CarrinhoService : ICarrinhoService
    {
        private readonly ICarrinhoRepository _carrinhoRepository;
        private readonly IUsuarioRepository _usuariosRepository;

        public CarrinhoService(ICarrinhoRepository carrinhoRepository, IUsuarioRepository usuariosRepository)
        {
            _carrinhoRepository = carrinhoRepository;
            _usuariosRepository = usuariosRepository;
        }

        public async Task<bool> AdicionarProdutoCarrinho(long usuarioId, Produto produto, int quantidade)
        {
            var usuario = await _usuariosRepository.Retornar(usuarioId);
            if (usuario == null)
            {
                throw new KeyNotFoundException("Usuário não encontrado.");
            }

            var carrinho = await _carrinhoRepository.RetornarPorUsuario(usuarioId)
                ?? new Carrinho
                {
                    Usuario = usuario,
                    Itens = new List<ItemCarrinho>()
                };

            var itemExistente = carrinho.Itens.FirstOrDefault(i => i.Produto.Id == produto.Id);
            if (itemExistente != null)
            {
                itemExistente.Quantidade += quantidade;
            }
            else
            {
                carrinho.Itens.Add(new ItemCarrinho
                {
                    Produto = produto,
                    Quantidade = quantidade
                });
            }

            carrinho.Total = carrinho.Itens.Sum(i => i.Subtotal);
            if (carrinho.Id == 0)
            {
                await _carrinhoRepository.Incluir(carrinho);
            }
            else
            {
                await _carrinhoRepository.Atualizar(carrinho);
            }

            return true;
        }

        public async Task<bool> RemoverProdutoCarrinho(long usuarioId, long produtoId)
        {
            var carrinho = await _carrinhoRepository.RetornarPorUsuario(usuarioId);
            if (carrinho == null)
            {
                throw new KeyNotFoundException("Carrinho não encontrado.");
            }

            var itemRemover = carrinho.Itens.FirstOrDefault(i => i.Produto.Id == produtoId);
            if (itemRemover == null)
            {
                throw new KeyNotFoundException("Produto não encontrado no carrinho.");

            }

            carrinho.Itens.Remove(itemRemover);
            carrinho.Total = carrinho.Itens.Sum(i => i.Subtotal);

            await _carrinhoRepository.Atualizar(carrinho);

            return true;
        }

        public async Task<Carrinho> ObterCarrinhoPorUsuario(long usuarioId)
        {
            return await _carrinhoRepository.RetornarPorUsuario(usuarioId) ?? throw new KeyNotFoundException("Carrinho não encontrado.");
        }

        public async Task<Pedido> ConverterCarrinhoEmPedido(long usuarioId)
        {
            var carrinho = await _carrinhoRepository.RetornarPorUsuario(usuarioId) ?? throw new KeyNotFoundException("Carrinho não encontrado.");

            if (!carrinho.Itens.Any())
            {
                throw new KeyNotFoundException("Carrinho está vazio.");
            }

            var pedido = new Pedido
            {
                Usuario = carrinho.Usuario,
                Produtos = carrinho.Itens.Select(i => i.Produto).ToList(),
                DataPedido = DateTime.Now,
                Status = StatusPedido.Pendente,
                Total = carrinho.Total
            };

            return pedido;
        }
    }
}
