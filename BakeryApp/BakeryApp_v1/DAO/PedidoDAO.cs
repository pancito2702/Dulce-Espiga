using BakeryApp_v1.DTO;
using BakeryApp_v1.Models;

namespace BakeryApp_v1.DAO;

public interface PedidoDAO
{
    public Task Guardar(Pedido pedido);

    public Task Eliminar(Pedido pedido);

    public Task Editar(Pedido pedido);

    public Task<IEnumerable<PedidoDTO>> ObtenerTodosLosPedidos(int pagina);

    public Task<IEnumerable<PedidoDTO>> ObtenerPedidoPorCliente(int idCliente);

    public Task<Pedido> ObtenerPedidoPorId(int idPedido);

    public Task<int> ContarTotalPedidos();

    public Task<PedidoDTO> ObtenerPedidoPorIdDTO(int idPedido);

    public Task<int> ObtenerCantidadPedidosNuevos();
}
