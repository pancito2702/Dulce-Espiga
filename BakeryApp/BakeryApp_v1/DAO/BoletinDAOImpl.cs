using BakeryApp_v1.DTO;
using BakeryApp_v1.Models;
using Microsoft.EntityFrameworkCore;
using PagedList;

namespace BakeryApp_v1.DAO
{
    public class BoletinDAOImpl : BoletinDAO
    {
        private readonly BakeryAppContext dbContext;

        public BoletinDAOImpl(BakeryAppContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Guardar(Boletin boletin)
        {
            dbContext.Add(boletin);
            await dbContext.SaveChangesAsync();
        }

        public async Task Eliminar(Boletin boletin)
        {
            dbContext.Remove(boletin);
            await dbContext.SaveChangesAsync();
        }

        public async Task<Boletin> ObtenerBoletinPorId(int idBoletin)
        {
            Boletin boletinBuscado = await dbContext.Boletins.Include(Boletin => Boletin.IdUsuarioNavigation).FirstOrDefaultAsync(Boletin => Boletin.IdBoletin == idBoletin);
            return boletinBuscado;
        }

        public async Task<IEnumerable<BoletinDTO>> ObtenerUsuariosBoletinPorPagina(int pagina)
        {
            int numeroDeElementosPorPagina = 10;

            IPagedList<BoletinDTO> todosLosUsuariosBoletin = dbContext.Boletins.OrderBy(Boletin => Boletin.IdBoletin).Include(Boletin => Boletin.IdUsuarioNavigation).Select(Boletin => new BoletinDTO
            {
                IdBoletin = Boletin.IdBoletin,
                IdBoletinNoticias = Boletin.IdBoletinNoticias,
                IdUsuario = Boletin.IdUsuario,
                Persona = new PersonaDTO
                {
                    Nombre = Boletin.IdUsuarioNavigation.Nombre,
                    Correo = Boletin.IdUsuarioNavigation.Correo,
                    PrimerApellido = Boletin.IdUsuarioNavigation.PrimerApellido,
                    SegundoApellido = Boletin.IdUsuarioNavigation.SegundoApellido,
                    Telefono = Boletin.IdUsuarioNavigation.Telefono,
                }
            }).ToPagedList(pageNumber: pagina, pageSize: numeroDeElementosPorPagina);
            return todosLosUsuariosBoletin;
        }

        public async Task<Boletin> VerificarUsuarioEnBoletin(int idUsuario)
        {
            Boletin boletinUsuario = await dbContext.Boletins.FirstOrDefaultAsync(Boletin => Boletin.IdUsuario == idUsuario);
            return boletinUsuario;
        }


        public async Task<int> ContarTotalBoletines()
        {
            int totalBoletines = await dbContext.Boletins.CountAsync();
            return totalBoletines;
        }

        public async Task<IEnumerable<Boletin>> ObtenerBoletinTodosLosUsuarios()
        {
            IEnumerable<Boletin> todosLosBoletinesUsuario = await dbContext.Boletins.Include(Boletin => Boletin.IdUsuarioNavigation).ToListAsync();
            return todosLosBoletinesUsuario;
        }
    }
}
