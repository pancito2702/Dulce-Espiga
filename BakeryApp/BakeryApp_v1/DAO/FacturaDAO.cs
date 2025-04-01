using BakeryApp_v1.DTO;
using BakeryApp_v1.Models;

namespace BakeryApp_v1.DAO
{
    public interface FacturaDAO
    {
        public Task Guardar(Factura factura);

        public Task Eliminar(Factura factura);

        public Task Editar(Factura factura);

        public Task<FacturaDTO> ObtenerPorIdFactura(int idFactura);

        public Task<Factura> ObtenerPorIdFacturaNormal(int idFactura);

        public Task<Factura> ObtenerPorIdPedido(int idPedido);

        public Task<IEnumerable<FacturaDTO>> ObtenerFacturasPorPagina(int pagina);

        public Task<int> ContarTotalPaginas();
    }
}
