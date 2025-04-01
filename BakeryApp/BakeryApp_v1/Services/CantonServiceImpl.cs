using BakeryApp_v1.DAO;
using BakeryApp_v1.Models;

namespace BakeryApp_v1.Services
{
    public class CantonServiceImpl : CantonService
    {
        private readonly CantonDAO cantonDAO;
        public CantonServiceImpl(CantonDAO cantonDAO)
        { 
            this.cantonDAO = cantonDAO;
        }

        public async Task<IEnumerable<Cantone>> ObtenerTodasLosCantonesPorProvincia(Provincia provincia)
        {
            IEnumerable<Cantone> cantonesPorProvincia = await cantonDAO.ObtenerTodasLosCantonesPorProvincia(provincia);

            return cantonesPorProvincia;
        }
    }
}
