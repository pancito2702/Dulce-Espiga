using BakeryApp_v1.DTO;
using BakeryApp_v1.Models;

namespace BakeryApp_v1.DAO
{
    public interface NotaCreditoDAO
    {
        public Task Guardar(Notascredito notasCredito);


        public Task<Notascredito> ObtenerNotaCreditoPorIdFactura(int idFactura);

        public Task<NotaCreditoDTO> ObtenerNotaCreditoDTOPorIdFactura(int idFactura);
    }
}
