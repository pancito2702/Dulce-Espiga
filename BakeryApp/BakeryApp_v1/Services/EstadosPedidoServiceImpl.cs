using BakeryApp_v1.DAO;
using BakeryApp_v1.Models;

namespace BakeryApp_v1.Services
{
    public class EstadosPedidoServiceImpl : EstadosPedidoService
    {
        private readonly EstadosPedidoDAO estadosPedidoDAO;

        public EstadosPedidoServiceImpl(EstadosPedidoDAO estadosPedidoDAO)
        {
            this.estadosPedidoDAO = estadosPedidoDAO;
        }

        public async Task<IEnumerable<Estadospedido>> ObtenerTodosLosEstadosPedido()
        {
            IEnumerable<Estadospedido> todosLosEstadosPedido = await estadosPedidoDAO.ObtenerTodosLosEstadosPedido();
            return todosLosEstadosPedido;
        }
    }
}
