using BakeryApp_v1.Models;
using System.Data.Entity;

namespace BakeryApp_v1.DAO
{
    public class BoletinNoticiasDAOImpl : BoletinNoticiasDAO
    {
        private readonly BakeryAppContext dbContext;

        public BoletinNoticiasDAOImpl(BakeryAppContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<Boletinnoticia>> ObtenerTodosLosBoletines()
        {
            IEnumerable<Boletinnoticia> todosLosBoletinesDeNoticias = await dbContext.Boletinnoticias.ToListAsync();
            return todosLosBoletinesDeNoticias;
        }
    }
}
