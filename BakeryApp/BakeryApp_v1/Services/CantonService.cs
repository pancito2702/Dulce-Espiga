using BakeryApp_v1.Models;

namespace BakeryApp_v1.Services;

public interface CantonService
{
    public Task<IEnumerable<Cantone>> ObtenerTodasLosCantonesPorProvincia(Provincia provincia); 



}
