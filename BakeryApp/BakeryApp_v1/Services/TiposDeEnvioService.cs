using BakeryApp_v1.Models;

namespace BakeryApp_v1.Services;

public interface TiposDeEnvioService
{
    public Task<IEnumerable<Tiposenvio>> ObtenerTodosLosTiposDeEnvio();

}
