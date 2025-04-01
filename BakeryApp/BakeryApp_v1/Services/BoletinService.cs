using BakeryApp_v1.DTO;
using BakeryApp_v1.Models;

namespace BakeryApp_v1.Services
{
    public interface BoletinService
    {
        public Task Guardar(Boletin boletin);

        public Task Eliminar(Boletin boletin);

        public Task<Boletin> ObtenerBoletinPorId(int idBoletin);
        public Task<IEnumerable<BoletinDTO>> ObtenerUsuariosBoletinPorPagina(int pagina);

        public Task<Boletin> VerificarUsuarioEnBoletin(int idUsuario);

        public Task<int> CalcularTotalPaginas();

        public Task<IEnumerable<Boletin>> ObtenerBoletinTodosLosUsuarios();

    }
}
