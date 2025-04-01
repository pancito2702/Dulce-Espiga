using BakeryApp_v1.Models;
using Microsoft.EntityFrameworkCore;
namespace BakeryApp_v1.DAO
{
    public class LineaFacturaDAOImpl : LineaFacturaDAO
    {
        private readonly BakeryAppContext dbContext;

        public LineaFacturaDAOImpl(BakeryAppContext dbContext)
        {
            this.dbContext = dbContext;
        }   

        public async Task Guardar(Detallefactura detallefactura)
        {
            dbContext.Add(detallefactura);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Detallefactura>> ObtenerTodosLosDetallesPorFactura(int idFactura)
        {
            IEnumerable<Detallefactura> todosLosDetalleFacturaPorPedido = await dbContext.Detallefacturas.Where(Factura => Factura.IdFactura == idFactura).ToListAsync();
            return todosLosDetalleFacturaPorPedido;
        }
    }
}
