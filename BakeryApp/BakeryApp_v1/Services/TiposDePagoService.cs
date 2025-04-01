using BakeryApp_v1.Models;

namespace BakeryApp_v1.Services;

public interface TiposDePagoService
{
    public Task<IEnumerable<Tipospago>> ObtenerTodosLosTiposDePago();

}
