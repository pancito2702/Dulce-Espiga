using BakeryApp_v1.Models;

namespace BakeryApp_v1.DAO
{
    public interface LineaFacturaDAO 
    {
        public Task Guardar(Detallefactura detallefactura);
        public Task<IEnumerable<Detallefactura>> ObtenerTodosLosDetallesPorFactura(int idFactura);

    }
}
