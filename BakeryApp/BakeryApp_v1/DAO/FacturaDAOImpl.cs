using BakeryApp_v1.DTO;
using BakeryApp_v1.Models;
using PagedList;
using Microsoft.EntityFrameworkCore;

namespace BakeryApp_v1.DAO
{
    public class FacturaDAOImpl : FacturaDAO
    {
        private readonly BakeryAppContext dbContext;

        public FacturaDAOImpl(BakeryAppContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Eliminar(Factura factura)
        {
            dbContext.Remove(factura);
            await dbContext.SaveChangesAsync();
        }

        public async Task Guardar(Factura factura)
        {
            dbContext.Add(factura);
            await dbContext.SaveChangesAsync();
        }


        public async Task Editar(Factura factura)
        {
            dbContext.Update(factura);
            await dbContext.SaveChangesAsync();
        }
        public async Task<IEnumerable<FacturaDTO>> ObtenerFacturasPorPagina(int pagina)
        {
            int numeroDeElementosPorPagina = 9;

            IPagedList<FacturaDTO> todasLasFacturas = dbContext.Facturas
            .Include(Factura => Factura.IdPedidoNavigation)
            .ThenInclude(Factura => Factura.IdTipoPagoNavigation)
            .Include(Factura => Factura.IdPedidoNavigation)
            .ThenInclude(Factura => Factura.IdPersonaNavigation)
            .Include(Factura => Factura.IdPedidoNavigation)
            .ThenInclude(Factura => Factura.IdEstadoPedidoNavigation)
            .Include(Factura => Factura.Detallefacturas)
            .ThenInclude(Factura => Factura.IdPedidoProductoNavigation)
            .ThenInclude(Factura => Factura.IdProductoNavigation)
            .Include(Factura => Factura.Notascreditos)
            .Select(Factura => new FacturaDTO
            {
                IdFactura = Factura.IdFactura,
                IdPedido = Factura.IdPedido,
                FechaFactura = Factura.FechaFactura.ToString("yyyy/MM/dd HH:mm:ss"),
                TotalPagar = Factura.TotalPagar,
                Pedido = new PedidoDTO
                {
                    Persona = new PersonaDTO
                    {
                        Correo = Factura.IdPedidoNavigation.IdPersonaNavigation.Correo,
                        Nombre = Factura.IdPedidoNavigation.IdPersonaNavigation.Nombre
                    },
                    EstadoPedido = new EstadoPedidoDTO
                    {
                        IdEstadoPedido = Factura.IdPedidoNavigation.IdEstadoPedidoNavigation.IdEstadoPedido,
                        NombreEstado = Factura.IdPedidoNavigation.IdEstadoPedidoNavigation.NombreEstado,

                    },
                    TipoPago = new TipoPagoDTO
                    {
                        IdTipoPago = Factura.IdPedidoNavigation.IdTipoPagoNavigation.IdTipoPago,
                        NombreTipo = Factura.IdPedidoNavigation.IdTipoPagoNavigation.NombreTipo,
                    }
                },
                DetalleFactura = Factura.Detallefacturas.Select(detalle => new DetalleFacturaDTO
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
                            PrecioProducto = detalle.IdPedidoProductoNavigation.IdProductoNavigation.PrecioProducto,
                        }
                    }
                }).ToList(),
                NotasCreditos = Factura.Notascreditos.Select(nota => new NotaCreditoDTO
                {
                    IdFactura = nota.IdFactura
                }).ToList()
            }).ToPagedList(pageNumber: pagina, pageSize: numeroDeElementosPorPagina);

            return todasLasFacturas;

        }

        public async Task<Factura> ObtenerPorIdFacturaNormal(int idFactura)
        {
            Factura facturaBuscada = await dbContext.Facturas.Include(Factura => Factura.IdPedidoNavigation).FirstOrDefaultAsync(Factura => Factura.IdFactura == idFactura);
            return facturaBuscada;
        }
        public async Task<FacturaDTO> ObtenerPorIdFactura(int idFactura)
        {
            Factura facturaBuscada = await dbContext.Facturas.Include(Factura => Factura.IdPedidoNavigation)
            .ThenInclude(Factura => Factura.IdTipoPagoNavigation)
            .Include(Factura => Factura.IdPedidoNavigation)
            .ThenInclude(Factura => Factura.IdPersonaNavigation)
            .Include(Factura => Factura.IdPedidoNavigation)
            .ThenInclude(Factura => Factura.IdEstadoPedidoNavigation)
            .Include(Factura => Factura.IdPedidoNavigation)
            .ThenInclude(Factura => Factura.IdTipoEnvioNavigation)
            .Include(Factura => Factura.Detallefacturas)
            .ThenInclude(Factura => Factura.IdPedidoProductoNavigation)
            .ThenInclude(Factura => Factura.IdProductoNavigation).FirstOrDefaultAsync(Factura => Factura.IdFactura == idFactura);
            return FacturaDTO.ConvertirFacturaAFacturaDTO(facturaBuscada);
        }

        public async Task<Factura> ObtenerPorIdPedido(int idPedido)
        {
            Factura facturaBuscada = await dbContext.Facturas.Include(Factura => Factura.IdPedidoNavigation).FirstOrDefaultAsync(Factura => Factura.IdPedido == idPedido);
            return facturaBuscada;
        }

        public async Task<int> ContarTotalPaginas()
        {
            int totalFacturas = await dbContext.Facturas.CountAsync();
            return totalFacturas;
        }
    }
}
