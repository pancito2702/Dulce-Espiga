using BakeryApp_v1.Models;

namespace BakeryApp_v1.DAO;

public interface DistritoDAO
{
    public Task<IEnumerable<Distrito>> ObtenerTodasLosDistritosPorCanton(Cantone canton);


}
