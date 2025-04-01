using BakeryApp_v1.DAO;
using BakeryApp_v1.DTO;
using BakeryApp_v1.Models;

namespace BakeryApp_v1.Services
{
    public class ProductoPedidoServiceImpl : ProductoPedidoService
    {
        private readonly ProductoPedidoDAO productoPedidoDAO;

        public ProductoPedidoServiceImpl(ProductoPedidoDAO productoPedidoDAO)
        {
            this.productoPedidoDAO = productoPedidoDAO;
        }

        public async Task Guardar(Pedidoproducto productoPedido)
        {
            await productoPedidoDAO.Guardar(productoPedido);
        }

        public async Task<IEnumerable<Pedidoproducto>> ObtenerTodosLosProductosPorPedido(int idPedido)
        {
            IEnumerable<Pedidoproducto> todosLosProductosPorPedido = await productoPedidoDAO.ObtenerTodosLosProductosPorPedido(idPedido);
            return todosLosProductosPorPedido;
        }

        public async Task<IEnumerable<ProductoPedidoDTO>> ObtenerProductosMasVendidos()
        {
            IEnumerable<ProductoPedidoDTO> top3Productos = await productoPedidoDAO.ObtenerProductosMasVendidos();
            return top3Productos;
        }
    }
}
