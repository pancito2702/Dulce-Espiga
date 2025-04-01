using BakeryApp_v1.DTO;
using BakeryApp_v1.Models;
using BakeryApp_v1.ViewModels;

namespace BakeryApp_v1.DAO;

public interface PedidoService
{
    public Task Guardar(Pedido pedido);

    public Task Eliminar(Pedido pedido);

    public Task Editar(Pedido pedido);


    public Task<IEnumerable<PedidoDTO>> ObtenerTodosLosPedidos(int pagina);
    public Task<IEnumerable<PedidoDTO>> ObtenerPedidoPorCliente(int idCliente);

    public Task<Pedido> ObtenerPedidoPorId(int idPedido);

    public bool VerificarDatosVaciosOIncorrectos(Pedido pedido);


    public Task<string> CrearSesionCheckoutStripe(PedidoViewModel pedido, IEnumerable<CarritoDTO> todosLosElementosDelCarrito, Pedido pedio);

    public decimal CalcularTotalPedido(IEnumerable<CarritoDTO> elementosCarrito);

    public decimal CalcularIva(decimal total, PedidoViewModel pedido);

    public string ObtenerTipoPagoPedido(PedidoViewModel pedido);

    public string ObtenerTipoEnvioPedido(PedidoViewModel pedido);

    public Task<int> CalcularTotalPaginas();

    public Task<PedidoDTO> ObtenerPedidoPorIdDTO(int idPedido);

    public bool VerificarPedidoPerteneceUsuario(Pedido pedido, int idUsuario);

    public Task<int> ObtenerCantidadPedidosNuevos();
}
