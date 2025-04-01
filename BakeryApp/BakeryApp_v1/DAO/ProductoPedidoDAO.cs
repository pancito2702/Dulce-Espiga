using BakeryApp_v1.DTO;
using BakeryApp_v1.Models;

namespace BakeryApp_v1.DAO
{
    public interface ProductoPedidoDAO
    {
        public Task Guardar(Pedidoproducto productoPedido);

        public Task<IEnumerable<Pedidoproducto>> ObtenerTodosLosProductosPorPedido(int idPedido);

        public Task<IEnumerable<ProductoPedidoDTO>> ObtenerProductosMasVendidos();
    }
}
