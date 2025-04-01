using BakeryApp_v1.DAO;
using BakeryApp_v1.Models;

namespace BakeryApp_v1.Services
{
    public class ProvinciaServiceImpl : ProvinciaService
    {
        private readonly ProvinciaDAO provinciaDAO;
        public ProvinciaServiceImpl(ProvinciaDAO provinciaDAO)
        { 
            this.provinciaDAO = provinciaDAO;
        }

        public async Task<IEnumerable<Provincia>> ObtenerTodasLasProvincias()
        {
            IEnumerable<Provincia> todasLasProvincias = await provinciaDAO.ObtenerTodasLasProvincias(); 
            return todasLasProvincias;  
        }
    }
}
