using BakeryApp_v1.DAO;
using BakeryApp_v1.DTO;
using BakeryApp_v1.Models;
using System.Runtime.InteropServices;

namespace BakeryApp_v1.Services
{
    public class BoletinServiceImpl : BoletinService
    {
        private readonly BoletinDAO boletinDAO;
        public BoletinServiceImpl(BoletinDAO boletinDAO)
        {
            this.boletinDAO = boletinDAO;
        }

        public async Task Eliminar(Boletin boletin)
        {
            await boletinDAO.Eliminar(boletin);
        }

        public async Task Guardar(Boletin boletin)
        {
            await boletinDAO.Guardar(boletin);
        }

        public async Task<IEnumerable<BoletinDTO>> ObtenerUsuariosBoletinPorPagina(int pagina)
        {
            IEnumerable<BoletinDTO> usuariosBoletinPorPagina = await boletinDAO.ObtenerUsuariosBoletinPorPagina(pagina);
            return usuariosBoletinPorPagina;
        }
        public async Task<Boletin> VerificarUsuarioEnBoletin(int idUsuario)
        {
            Boletin boletinUsuario = await boletinDAO.VerificarUsuarioEnBoletin(idUsuario);
            return boletinUsuario;
        }


        public async Task<Boletin> ObtenerBoletinPorId(int idBoletin)
        {
            Boletin boletinBuscada = await boletinDAO.ObtenerBoletinPorId(idBoletin);
            return boletinBuscada;
        }
        public async Task<int> CalcularTotalPaginas()
        {
            int totalBoletines = await boletinDAO.ContarTotalBoletines();
            int elementosPorPagina = 9;
            double totalPaginas = (double)totalBoletines / elementosPorPagina;
            totalPaginas = Math.Ceiling(totalPaginas);

            return (int)totalPaginas;
        }

        public async Task<IEnumerable<Boletin>> ObtenerBoletinTodosLosUsuarios()
        {
            IEnumerable<Boletin> todosLosUsuariosBoletines = await boletinDAO.ObtenerBoletinTodosLosUsuarios();
            return todosLosUsuariosBoletines;
        }
    }
}
