using BakeryApp_v1.Models;

namespace BakeryApp_v1.Services;

public interface DistritoService
{
    public Task<IEnumerable<Distrito>> ObtenerTodasLosDistritosPorCanton(Cantone canton); 



}
