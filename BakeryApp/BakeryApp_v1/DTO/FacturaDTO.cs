using BakeryApp_v1.Models;
using PagedList;

namespace BakeryApp_v1.DTO
{
    public class FacturaDTO
    {
        public int IdFactura { get; set; }

        public int IdPedido { get; set; }

        public decimal? TotalPagar { get; set; }

        public decimal? Iva { get; set; }

        public decimal? Envio { get; set; }
        public string FechaFactura { get; set; }

        public PedidoDTO Pedido { get; set; }

        public ICollection<DetalleFacturaDTO> DetalleFactura { get; set; } = new List<DetalleFacturaDTO>();

        public virtual ICollection<NotaCreditoDTO> NotasCreditos { get; set; } = new List<NotaCreditoDTO>();


        public static FacturaDTO ConvertirFacturaAFacturaDTO(Factura factura)
        {
            return new FacturaDTO
            {
                IdFactura = factura.IdFactura,
                IdPedido = factura.IdPedido,
                FechaFactura = factura.FechaFactura.ToString("yyyy/MM/dd HH:mm:ss"),
                TotalPagar = factura.TotalPagar,
                Pedido = new PedidoDTO
                {
                    Persona = new PersonaDTO
                    {
                        Correo = factura.IdPedidoNavigation.IdPersonaNavigation.Correo,
                        Nombre = factura.IdPedidoNavigation.IdPersonaNavigation.Nombre
                    },
                    EstadoPedido = new EstadoPedidoDTO
                    {
                        IdEstadoPedido = factura.IdPedidoNavigation.IdEstadoPedidoNavigation.IdEstadoPedido,
                        NombreEstado = factura.IdPedidoNavigation.IdEstadoPedidoNavigation.NombreEstado
                    },
                    TipoPago = new TipoPagoDTO
                    {
                        IdTipoPago = factura.IdPedidoNavigation.IdTipoPagoNavigation.IdTipoPago,
                        NombreTipo = factura.IdPedidoNavigation.IdTipoPagoNavigation.NombreTipo
                    },
                    TipoEnvio = new TipoEnvioDTO
                    {
                        IdTipoEnvio = factura.IdPedidoNavigation.IdTipoEnvioNavigation.IdTipoEnvio,
                        NombreTipo = factura.IdPedidoNavigation.IdTipoEnvioNavigation.NombreTipo
                    }
                },
                DetalleFactura = factura.Detallefacturas.Select(detalle => new DetalleFacturaDTO
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
            };
        }


        public static Factura ConvertirFacturaDTOAFactura(FacturaDTO factura)
        {
            return new Factura
            {
                IdFactura = factura.IdFactura,
            };
        }


    }
}
