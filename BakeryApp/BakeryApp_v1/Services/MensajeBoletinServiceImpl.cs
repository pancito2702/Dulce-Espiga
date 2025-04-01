using BakeryApp_v1.DAO;
using BakeryApp_v1.Models;

namespace BakeryApp_v1.Services
{
    public class MensajeBoletinServiceImpl : MensajeBoletinService
    {
        private readonly MensajesBoletinDAO mensajesBoletinDAO;

        public MensajeBoletinServiceImpl(MensajesBoletinDAO mensajesBoletinDAO)
        {
            this.mensajesBoletinDAO = mensajesBoletinDAO;
        }

        public async Task Guardar(Mensajesboletin mensaje)
        {
            await mensajesBoletinDAO.Guardar(mensaje);
        }


        public bool VerificarDatosVaciosONulos(Mensajesboletin mensaje)
        {
            if (string.IsNullOrEmpty(mensaje.Asunto) || string.IsNullOrEmpty(mensaje.Mensaje))
            {
                return false;
            }
            return true;
        }
    }
}
