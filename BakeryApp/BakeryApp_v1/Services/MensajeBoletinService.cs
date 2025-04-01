using BakeryApp_v1.Models;

namespace BakeryApp_v1.Services
{
    public interface MensajeBoletinService
    {
        public Task Guardar(Mensajesboletin mensaje);

        public bool VerificarDatosVaciosONulos(Mensajesboletin mensaje);
    }
}
