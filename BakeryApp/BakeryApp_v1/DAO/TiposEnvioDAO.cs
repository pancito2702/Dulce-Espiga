using BakeryApp_v1.Models;

namespace BakeryApp_v1.DAO;

public interface TiposEnvioDAO
{
    public Task<IEnumerable<Tiposenvio>> ObtenerTodosLosTiposDeEnvio();  

}
