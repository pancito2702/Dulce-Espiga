using BakeryApp_v1.Models;
using Microsoft.EntityFrameworkCore;

namespace BakeryApp_v1.DAO
{
    public class EstadosPedidoDAOImpl : EstadosPedidoDAO
    {
        private readonly BakeryAppContext dbContext;
        public EstadosPedidoDAOImpl(BakeryAppContext dbContext)
        {
            this.dbContext = dbContext; 
        }

        public async Task<IEnumerable<Estadospedido>> ObtenerTodosLosEstadosPedido()
        {
            IEnumerable<Estadospedido> todosLosEstadosPedido = await dbContext.Estadospedidos.ToListAsync();
            return todosLosEstadosPedido;
        }
    }
}
