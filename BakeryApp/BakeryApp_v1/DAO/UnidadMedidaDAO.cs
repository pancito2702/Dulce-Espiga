using BakeryApp_v1.Models;

namespace BakeryApp_v1.DAO;

public interface UnidadMedidaDAO
{




    public Task<IEnumerable<Unidadesmedidum>> ObtenerTodasLasUnidadesDeMedida();


}
