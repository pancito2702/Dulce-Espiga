using BakeryApp_v1.DAO;
using BakeryApp_v1.Models;

namespace BakeryApp_v1.Services
{
    public class DistritoServiceImpl : DistritoService
    {
        private readonly DistritoDAO distritoDAO;
        public DistritoServiceImpl(DistritoDAO distritoDAO)
        { 
            this.distritoDAO = distritoDAO;
        }

        public async Task<IEnumerable<Distrito>> ObtenerTodasLosDistritosPorCanton(Cantone canton)
        {
            IEnumerable<Distrito> distritosPorCanton = await distritoDAO.ObtenerTodasLosDistritosPorCanton(canton);
            return distritosPorCanton;  
        }
    }
}
