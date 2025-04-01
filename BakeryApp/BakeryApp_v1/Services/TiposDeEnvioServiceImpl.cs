using BakeryApp_v1.DAO;
using BakeryApp_v1.Models;

namespace BakeryApp_v1.Services
{
    public class TiposDeEnvioServiceImpl : TiposDeEnvioService
    {
        private readonly TiposEnvioDAO tiposEnvioDAO;

        public TiposDeEnvioServiceImpl(TiposEnvioDAO tiposEnvioDAO)
        {
            this.tiposEnvioDAO = tiposEnvioDAO;



        }

        public async Task<IEnumerable<Tiposenvio>> ObtenerTodosLosTiposDeEnvio()
        {
            IEnumerable<Tiposenvio> todosLosTiposDeEnvio = await tiposEnvioDAO.ObtenerTodosLosTiposDeEnvio();
            return todosLosTiposDeEnvio;
        }
    }
}
