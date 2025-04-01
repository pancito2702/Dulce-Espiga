using BakeryApp_v1.Models;

namespace BakeryApp_v1.DAO
{
    public class MensajeBoletinDAOImpl : MensajesBoletinDAO
    {
        private readonly BakeryAppContext dbContext;

        public MensajeBoletinDAOImpl(BakeryAppContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task Guardar(Mensajesboletin mensaje)
        {
            dbContext.Add(mensaje);
            await dbContext.SaveChangesAsync(); 
        }
    }
}
