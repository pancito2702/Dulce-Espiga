using BakeryApp_v1.DTO;
using BakeryApp_v1.Models;
using Microsoft.EntityFrameworkCore;
using PagedList;


namespace BakeryApp_v1.DAO;

public class PedidoDAOImpl : PedidoDAO
{
    private readonly BakeryAppContext dbContext;

    public PedidoDAOImpl(BakeryAppContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task Guardar(Pedido pedido)
    {
        dbContext.Add(pedido);
        await dbContext.SaveChangesAsync();
    }

    public async Task Editar(Pedido pedido)
    {
        dbContext.Update(pedido);
        await dbContext.SaveChangesAsync();
    }

    public async Task Eliminar(Pedido pedido)
    {
        dbContext.Remove(pedido);
        await dbContext.SaveChangesAsync();
    }

    public async Task<int> ObtenerCantidadPedidosNuevos()
    {
        // Pedido recibido, preparando o pagado
        int cantidadPedidos = await dbContext.Pedidos.Where(Pedido => Pedido.IdEstadoPedido == 1 || Pedido.IdEstadoPedido == 2 || Pedido.IdEstadoPedido == 4).CountAsync();
        return cantidadPedidos;
    }

    public async Task<IEnumerable<PedidoDTO>> ObtenerPedidoPorCliente(int idCliente)
    {
        IEnumerable<PedidoDTO> pedidosPorCliente =  await dbContext.Pedidos.OrderByDescending(Pedido => Pedido.FechaPedido).Include(Pedido => Pedido.IdEstadoPedidoNavigation).Include(Pedido => Pedido.IdTipoPagoNavigation).Where(Pedido => Pedido.IdPersona == idCliente).Select(
            Pedido => new PedidoDTO
            {
                IdPedido = Pedido.IdPedido,
                IdEstadoPedido = Pedido.IdEstadoPedido,
                IdPersona = Pedido.IdPersona,
                IdTipoPago = Pedido.IdTipoPago,
                IdDireccion = Pedido.IdDireccion,
                FechaPedido = Pedido.FechaPedido.ToString("yyyy/MM/dd HH:mm:ss"),
                EstadoPedido = new EstadoPedidoDTO
                {
                    IdEstadoPedido = Pedido.IdEstadoPedidoNavigation.IdEstadoPedido,
                    NombreEstado = Pedido.IdEstadoPedidoNavigation.NombreEstado
                },
                TipoPago = new TipoPagoDTO
                {
                    IdTipoPago = Pedido.IdTipoPagoNavigation.IdTipoPago,
                    NombreTipo = Pedido.IdTipoPagoNavigation.NombreTipo
                },
            }).ToListAsync();
        return pedidosPorCliente;
    }

    public async Task<Pedido> ObtenerPedidoPorId(int idPedido)
    {
        Pedido pedidoBuscado = await dbContext.Pedidos.FirstOrDefaultAsync(Pedido => Pedido.IdPedido == idPedido);
        return pedidoBuscado;
    }

    public async Task<IEnumerable<PedidoDTO>> ObtenerTodosLosPedidos(int pagina)
    {
        int numeroDeElementosPorPagina = 10;
        IPagedList<PedidoDTO> todosLosPedidosPorPagina = dbContext.Pedidos.Include(Pedido => Pedido.IdEstadoPedidoNavigation).OrderByDescending(Pedido => Pedido.FechaPedido).Select(
            Pedido => new PedidoDTO
            {
                IdPedido = Pedido.IdPedido,
                IdEstadoPedido = Pedido.IdEstadoPedido,
                IdPersona = Pedido.IdPersona,
                IdTipoPago = Pedido.IdTipoPago,
                IdDireccion = Pedido.IdDireccion,
                FechaPedido = Pedido.FechaPedido.ToString("yyyy/MM/dd HH:mm:ss"),
                EstadoPedido = new EstadoPedidoDTO
                {
                    IdEstadoPedido = Pedido.IdEstadoPedidoNavigation.IdEstadoPedido,
                    NombreEstado = Pedido.IdEstadoPedidoNavigation.NombreEstado
                },
            }).ToPagedList(pageNumber: pagina, pageSize: numeroDeElementosPorPagina);
        return todosLosPedidosPorPagina;
    }



    public async Task<int> ContarTotalPedidos()
    {
        int totalPedidos = await dbContext.Pedidos.CountAsync();
        return totalPedidos;
    }

    public async Task<PedidoDTO> ObtenerPedidoPorIdDTO(int idPedido)
    {
        Pedido pedidoBuscado = await dbContext.Pedidos.Include(Pedido => Pedido.IdDireccionNavigation).ThenInclude(Pedido => Pedido.IdProvinciaNavigation).ThenInclude(Pedido => Pedido.Cantones).ThenInclude(Pedido => Pedido.Distritos)
            .Include(Pedido => Pedido.IdTipoEnvioNavigation).Include(Pedido => Pedido.IdEstadoPedidoNavigation).Include(Pedido => Pedido.IdTipoPagoNavigation).Include(Pedido => Pedido.Pedidoproductos)
            .ThenInclude(Pedido => Pedido.IdProductoNavigation).Include(Pedido => Pedido.Pagossinpes).Include(Pedido => Pedido.IdPersonaNavigation).FirstOrDefaultAsync(Pedido => Pedido.IdPedido == idPedido);
        return PedidoDTO.ConvertirPedidoAPedidoDTO(pedidoBuscado);
    }
}
