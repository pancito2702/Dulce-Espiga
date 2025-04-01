using BakeryApp_v1.DAO;
using BakeryApp_v1.DTO;
using BakeryApp_v1.Models;

namespace BakeryApp_v1.Services
{
    public class FacturaServiceImpl : FacturaService
    {
        private readonly FacturaDAO facturaDAO;
        private readonly ProductoPedidoService productoPedidoService;
        private readonly LineaFacturaService lineaFacturaService;
        public FacturaServiceImpl(FacturaDAO facturaDAO, ProductoPedidoService productoPedidoService, LineaFacturaService lineaFacturaService = null)
        {
            this.facturaDAO = facturaDAO;
            this.productoPedidoService = productoPedidoService;
            this.lineaFacturaService = lineaFacturaService;
        }




        public async Task Eliminar(Factura factura)
        {
            await facturaDAO.Eliminar(factura);
        }


        public async Task<IEnumerable<FacturaDTO>> ObtenerFacturasPorPagina(int pagina)
        {
            IEnumerable<FacturaDTO> facturasPorPagina = await facturaDAO.ObtenerFacturasPorPagina(pagina);
            return facturasPorPagina;
        }

        public async Task<FacturaDTO> ObtenerPorIdFactura(int idFactura)
        {
            FacturaDTO facturaBuscada = await facturaDAO.ObtenerPorIdFactura(idFactura);
            return facturaBuscada;
        }

        public async Task<Factura> ObtenerPorIdPedido(int idPedido)
        {
            Factura facturaBuscada = await facturaDAO.ObtenerPorIdPedido(idPedido);
            return facturaBuscada;
        }

        public async Task<int> ContarTotalPaginas()
        {
            int totalPaginas = await facturaDAO.ContarTotalPaginas();
            return totalPaginas;
        }

        public async Task<int> CalcularTotalPaginas()
        {
            int totalFacturas = await facturaDAO.ContarTotalPaginas();
            int elementosPorPagina = 10;
            double totalPaginas = (double)totalFacturas / elementosPorPagina;
            totalPaginas = Math.Ceiling(totalPaginas);

            return (int)totalPaginas;
        }



        public async Task<bool> GuardarFactura(Pedido pedido)
        {
            try
            {
                Factura facturaAGuardar = new Factura();
                facturaAGuardar.IdPedido = pedido.IdPedido;
                facturaAGuardar.FechaFactura = DateTime.Now;



                await facturaDAO.Guardar(facturaAGuardar);

                IEnumerable<Pedidoproducto> productosPedidoPorPedido = await productoPedidoService.ObtenerTodosLosProductosPorPedido(pedido.IdPedido);

                int contadorLineas = 1;
                decimal sumadorPrecio = 0;


                List<Detallefactura> detalleFactura = new List<Detallefactura>();   

                foreach (Pedidoproducto producto in productosPedidoPorPedido)
                {
                    detalleFactura.Add(new Detallefactura
                    {
                        Linea = contadorLineas,
                        IdFactura = facturaAGuardar.IdFactura,
                        IdPedidoProducto = producto.IdPedidoProducto,
                        TotalLinea = producto.CantidadProducto * producto.IdProductoNavigation.PrecioProducto
                    });
                    sumadorPrecio += producto.CantidadProducto * producto.IdProductoNavigation.PrecioProducto;
                    contadorLineas++;
                }

    

                // Si el envio es a domicilio
                if (pedido.IdTipoEnvio == 1)
                {
                    facturaAGuardar.Envio = 2500;
                    sumadorPrecio += 2500;
                }

                decimal iva = 0.14m;



                iva = sumadorPrecio * iva;

                decimal totalFinal = iva + sumadorPrecio;


                facturaAGuardar.TotalPagar = totalFinal;
                facturaAGuardar.Iva = iva;
                foreach (Detallefactura detallefactura in detalleFactura)
                {
                    await lineaFacturaService.Guardar(detallefactura);  
                }

                await facturaDAO.Editar(facturaAGuardar);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<Factura> ObtenerPorIdFacturaNormal(int idFactura) {
            Factura facturaBuscada = await facturaDAO.ObtenerPorIdFacturaNormal(idFactura);
            return facturaBuscada;
        }

    }
}
