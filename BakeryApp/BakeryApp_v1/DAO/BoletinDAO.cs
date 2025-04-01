using BakeryApp_v1.DTO;
using BakeryApp_v1.Models;

namespace BakeryApp_v1.DAO
{
    public interface BoletinDAO
    {
        public Task Guardar(Boletin boletin);

        public Task Eliminar(Boletin boletin);

        public Task<Boletin> ObtenerBoletinPorId(int idBoletin);  
        public Task<IEnumerable<BoletinDTO>> ObtenerUsuariosBoletinPorPagina(int pagina);

        public Task<IEnumerable<Boletin>> ObtenerBoletinTodosLosUsuarios();
        public Task<Boletin> VerificarUsuarioEnBoletin(int idUsuario);

        public Task<int> ContarTotalBoletines();

    }
}
