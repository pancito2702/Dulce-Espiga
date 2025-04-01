using BakeryApp_v1.Models;

namespace BakeryApp_v1.DTO
{
    public class ProductoPedidoDTO
    {
        public int IdPedidoProducto { get; set; }

        public int IdProducto { get; set; }

        public int IdPedido { get; set; }

        public int CantidadProducto { get; set; }

        public ProductoDTO Producto { get; set; } = null!;
    }
}
