using BakeryApp_v1.Models;

namespace BakeryApp_v1.Services;

public interface ProvinciaService
{
    public Task<IEnumerable<Provincia>> ObtenerTodasLasProvincias(); 



}
