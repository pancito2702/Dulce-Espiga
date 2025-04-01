using BakeryApp_v1.Models;

namespace BakeryApp_v1.Services;

public interface EstadosPedidoService
{
    public Task<IEnumerable<Estadospedido>> ObtenerTodosLosEstadosPedido();  

}
