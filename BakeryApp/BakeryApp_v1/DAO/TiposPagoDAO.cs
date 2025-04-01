using BakeryApp_v1.Models;

namespace BakeryApp_v1.DAO;

public interface TiposPagoDAO
{
    public Task<IEnumerable<Tipospago>> ObtenerTodosLosTiposDePago();  

}
