namespace BakeryApp_v1.DTO
{
    public class DetalleFacturaDTO
    {
        public int Linea { get; set; }

        public int IdFactura { get; set; }

        public int IdPedidoProducto { get; set; }

        public ProductoPedidoDTO ProductoPedido { get; set; }

        public decimal TotalLinea { get; set; }

    }
}
