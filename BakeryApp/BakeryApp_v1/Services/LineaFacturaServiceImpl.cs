using BakeryApp_v1.DAO;
using BakeryApp_v1.Models;

namespace BakeryApp_v1.Services
{
    public class LineaFacturaServiceImpl : LineaFacturaService
    {
        private readonly LineaFacturaDAO lineaFacturaDAO;

        public LineaFacturaServiceImpl(LineaFacturaDAO lineaFacturaDAO)
        {
            this.lineaFacturaDAO = lineaFacturaDAO;
        }

        public async Task Guardar(Detallefactura detallefactura)
        {
            await lineaFacturaDAO.Guardar(detallefactura);
        }

        public async Task<IEnumerable<Detallefactura>> ObtenerTodosLosDetallesPorFactura(int idFactura)
        {
            IEnumerable<Detallefactura> obtenerTodosLosDetalleFacturaPorFactura = await lineaFacturaDAO.ObtenerTodosLosDetallesPorFactura(idFactura); 
            return obtenerTodosLosDetalleFacturaPorFactura;
        }
    }
}
