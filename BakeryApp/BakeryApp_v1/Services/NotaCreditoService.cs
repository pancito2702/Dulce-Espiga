using BakeryApp_v1.DTO;
using BakeryApp_v1.Models;

namespace BakeryApp_v1.Services
{
    public interface NotaCreditoService
    {
        public Task Guardar(Notascredito notasCredito);


        public Task<Notascredito> ObtenerNotaCreditoPorIdFactura(int idNotaCredito);

        public Task<NotaCreditoDTO> ObtenerNotaCreditoDTOPorIdFactura(int idFactura);
    }
}
