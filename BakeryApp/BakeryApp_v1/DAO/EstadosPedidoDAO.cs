using BakeryApp_v1.Models;

namespace BakeryApp_v1.DAO;

public interface EstadosPedidoDAO
{
    public Task<IEnumerable<Estadospedido>> ObtenerTodosLosEstadosPedido();  

}
