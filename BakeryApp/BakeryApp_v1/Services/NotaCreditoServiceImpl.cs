using BakeryApp_v1.DAO;
using BakeryApp_v1.DTO;
using BakeryApp_v1.Models;

namespace BakeryApp_v1.Services
{
    public class NotaCreditoServiceImpl : NotaCreditoService
    {
        private readonly NotaCreditoDAO notaCreditoDAO;


        public NotaCreditoServiceImpl(NotaCreditoDAO notaCreditoDAO)
        {
            this.notaCreditoDAO = notaCreditoDAO;
        }

        public async Task Guardar(Notascredito notasCredito)
        {
            await notaCreditoDAO.Guardar(notasCredito);
        }

        public async Task<Notascredito> ObtenerNotaCreditoPorIdFactura(int idFactura)
        {
            Notascredito notaCreditoBuscada = await notaCreditoDAO.ObtenerNotaCreditoPorIdFactura(idFactura);
            return notaCreditoBuscada;
        }

        public async Task<NotaCreditoDTO> ObtenerNotaCreditoDTOPorIdFactura(int idFactura)
        {
            NotaCreditoDTO notaCreditoBuscadaDTO = await notaCreditoDAO.ObtenerNotaCreditoDTOPorIdFactura(idFactura);
            return notaCreditoBuscadaDTO;
        }
    }
}
