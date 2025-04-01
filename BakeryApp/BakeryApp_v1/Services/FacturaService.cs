using BakeryApp_v1.DTO;
using BakeryApp_v1.Models;

namespace BakeryApp_v1.Services
{
    public interface FacturaService
    {

        public Task Eliminar(Factura factura);

        public Task<FacturaDTO> ObtenerPorIdFactura(int idFactura);

        public Task<Factura> ObtenerPorIdPedido(int idPedido);

        public Task<IEnumerable<FacturaDTO>> ObtenerFacturasPorPagina(int pagina);

        public Task<int> ContarTotalPaginas();

        public Task<int> CalcularTotalPaginas();

        public Task<bool> GuardarFactura(Pedido pedido);

        public Task<Factura> ObtenerPorIdFacturaNormal(int idFactura);
    }
}
