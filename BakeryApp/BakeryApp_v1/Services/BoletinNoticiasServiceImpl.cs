using BakeryApp_v1.DAO;
using BakeryApp_v1.Models;

namespace BakeryApp_v1.Services
{
    public class BoletinNoticiasServiceImpl : BoletinNoticiasService
    {
        private readonly BoletinNoticiasDAO boletinNoticiasDAO;

        public BoletinNoticiasServiceImpl(BoletinNoticiasDAO boletinNoticiasDAO)
        {
            this.boletinNoticiasDAO = boletinNoticiasDAO;
        }

        public async Task<IEnumerable<Boletinnoticia>> ObtenerTodosLosBoletines()
        {
            IEnumerable<Boletinnoticia> todosLosBoletinesNoticias = await boletinNoticiasDAO.ObtenerTodosLosBoletines();
            return todosLosBoletinesNoticias;
        }
    }
}
