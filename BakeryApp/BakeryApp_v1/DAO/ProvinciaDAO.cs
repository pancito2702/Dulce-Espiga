using BakeryApp_v1.Models;

namespace BakeryApp_v1.DAO;

public interface ProvinciaDAO
{
    public Task<IEnumerable<Provincia>> ObtenerTodasLasProvincias();

}
