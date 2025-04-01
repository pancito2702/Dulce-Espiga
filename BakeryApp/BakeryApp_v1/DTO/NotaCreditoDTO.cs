using BakeryApp_v1.Models;

namespace BakeryApp_v1.DTO
{
    public class NotaCreditoDTO
    {
        public int IdNotaCredito { get; set; }

        public int IdFactura { get; set; }

        public FacturaDTO Factura { get; set; }

        public static NotaCreditoDTO ConvertirNotaCreditoANotaCreditoDTO(Notascredito notascredito)
        {
            return new NotaCreditoDTO
            {
                IdNotaCredito = notascredito.IdNotaCredito,
                IdFactura = notascredito.IdFactura,
                Factura = new FacturaDTO
                {
                    IdFactura = notascredito.IdFacturaNavigation.IdFactura,
                    FechaFactura = notascredito.IdFacturaNavigation.FechaFactura.ToString("yyyy/MM/dd HH:mm:ss"),
                    TotalPagar = notascredito.IdFacturaNavigation.TotalPagar,
                    Iva = notascredito.IdFacturaNavigation.Iva,
                    Envio = notascredito.IdFacturaNavigation.Envio,
                    Pedido = new PedidoDTO
                    {
                        IdPedido = notascredito.IdFacturaNavigation.IdPedidoNavigation.IdPedido,
                        Persona = new PersonaDTO
                        {
                            Correo = notascredito.IdFacturaNavigation.IdPedidoNavigation.IdPersonaNavigation.Correo,
                            Nombre = notascredito.IdFacturaNavigation.IdPedidoNavigation.IdPersonaNavigation.Nombre
                        },
                    },
                    DetalleFactura = notascredito.IdFacturaNavigation.Detallefacturas.Select(detalle => new DetalleFacturaDTO
                    {
                        Linea = detalle.Linea,
                        IdFactura = detalle.IdFactura,
                        IdPedidoProducto = detalle.IdPedidoProducto,
                        TotalLinea = detalle.TotalLinea,
                        ProductoPedido = new ProductoPedidoDTO
                        {
                            CantidadProducto = detalle.IdPedidoProductoNavigation.CantidadProducto,
                            Producto = new ProductoDTO
                            {
                                NombreProducto = detalle.IdPedidoProductoNavigation.IdProductoNavigation.NombreProducto,
                                PrecioProducto = detalle.IdPedidoProductoNavigation.IdProductoNavigation.PrecioProducto
                            }
                        }
                    }).ToList()
                }
            };
        }
    }
}
