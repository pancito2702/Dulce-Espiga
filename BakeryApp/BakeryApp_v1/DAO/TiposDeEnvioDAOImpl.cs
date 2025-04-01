using BakeryApp_v1.Models;
using Microsoft.EntityFrameworkCore;

namespace BakeryApp_v1.DAO
{
    public class TiposDeEnvioDAOImpl : TiposEnvioDAO
    {
        private readonly BakeryAppContext dbContext;
        public TiposDeEnvioDAOImpl(BakeryAppContext dbContext)
        {
            this.dbContext = dbContext; 
        }

        public async Task<IEnumerable<Tiposenvio>> ObtenerTodosLosTiposDeEnvio()
        {
            IEnumerable<Tiposenvio> todosLosTiposDeEnvio = await dbContext.Tiposenvios.ToListAsync();
            return todosLosTiposDeEnvio;
        }
    }
}
