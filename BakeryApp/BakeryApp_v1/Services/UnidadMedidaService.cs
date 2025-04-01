using BakeryApp_v1.Models;

namespace BakeryApp_v1.Services;

public interface UnidadMedidaService
{
    

    public Task<IEnumerable<Unidadesmedidum>> ObtenerTodasLasUnidadesDeMedida();


}
