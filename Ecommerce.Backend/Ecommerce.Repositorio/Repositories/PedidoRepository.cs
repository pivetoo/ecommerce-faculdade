using Ecommerce.Dominio.Entities;
using Ecommerce.Dominio.IRepository;
using Ecommerce.Repositorio.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repositorio.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly EcommerceDbContext _context;

        public PedidoRepository(EcommerceDbContext context)
        {
            _context = context;
        }

        public async Task<Pedido> Retornar(long id)
        {
            return await _context.Pedidos.FindAsync(id);
        }

        public async Task<IEnumerable<Pedido>> ConsultaPedidosPorUsuario(long usuarioId)
        {
            return await _context.Pedidos.Include(p => p.Produtos).Where(p => p.Usuario.Id == usuarioId).ToListAsync();
        }

        public async Task Incluir(Pedido pedido)
        {
            await _context.Pedidos.AddAsync(pedido);
            await _context.SaveChangesAsync();
        }

        public async Task Atualizar(Pedido pedido)
        {
            _context.Pedidos.Update(pedido);
            await _context.SaveChangesAsync();
        }

        public async Task Excluir(Pedido pedido)
        {
            await _context.Pedidos.FindAsync(pedido.Id);
            _context.Pedidos.Remove(pedido);
            await _context.SaveChangesAsync();
        }
    }
}
