using BakeryApp_v1.Models;

namespace BakeryApp_v1.Services
{
    public interface LineaFacturaService 
    {
        public Task Guardar(Detallefactura detallefactura);
        public Task<IEnumerable<Detallefactura>> ObtenerTodosLosDetallesPorFactura(int idFactura);

    }
}
