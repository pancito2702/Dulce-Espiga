using BakeryApp_v1.Models;

namespace BakeryApp_v1.DAO;

public interface CantonDAO
{
    public Task<IEnumerable<Cantone>> ObtenerTodasLosCantonesPorProvincia(Provincia provincia);


}
