using BakeryApp_v1.DTO;
using BakeryApp_v1.Models;
using Microsoft.EntityFrameworkCore;

namespace BakeryApp_v1.DAO
{
    public class NotaCreditoDAOImpl : NotaCreditoDAO
    {
        private readonly BakeryAppContext dbContext;

        public NotaCreditoDAOImpl(BakeryAppContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Guardar(Notascredito notasCredito)
        {
            dbContext.Add(notasCredito);
            await dbContext.SaveChangesAsync();
        }

        public async Task<Notascredito> ObtenerNotaCreditoPorIdFactura(int idFactura)
        {
            Notascredito notaCreditoBuscada = await dbContext.Notascreditos.FirstOrDefaultAsync(notaCredito => notaCredito.IdFactura == idFactura);
            return notaCreditoBuscada;
        }


        public async Task<NotaCreditoDTO> ObtenerNotaCreditoDTOPorIdFactura(int idFactura)
        {
            Notascredito notaCreditoBuscada = await dbContext.Notascreditos.Include(notaCredito => notaCredito.IdFacturaNavigation).ThenInclude(notaCredito => notaCredito.IdPedidoNavigation).ThenInclude(notaCredito => notaCredito.IdPersonaNavigation)
                .Include(notaCredito => notaCredito.IdFacturaNavigation).ThenInclude(notaCredito => notaCredito.Detallefacturas).ThenInclude(notaCredito => notaCredito.IdPedidoProductoNavigation).ThenInclude(notaCredito => notaCredito.IdProductoNavigation).
                FirstOrDefaultAsync(notaCreditoBuscada => notaCreditoBuscada.IdFactura == idFactura);


            return NotaCreditoDTO.ConvertirNotaCreditoANotaCreditoDTO(notaCreditoBuscada);  
        }
    }
}
