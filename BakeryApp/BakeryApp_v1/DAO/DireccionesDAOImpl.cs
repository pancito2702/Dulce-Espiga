using BakeryApp_v1.DTO;
using BakeryApp_v1.Models;
using Microsoft.EntityFrameworkCore;
using PagedList;

namespace BakeryApp_v1.DAO;

public class DireccionesDAOImpl : DireccionesDAO
{
    private readonly BakeryAppContext dbContext;

    public DireccionesDAOImpl(BakeryAppContext dbContext)
    {
        this.dbContext = dbContext;
    }


    public async Task Guardar(Direccionesusuario direccion)
    {
        dbContext.Add(direccion);
        await dbContext.SaveChangesAsync();
    }

    public async Task Editar(Direccionesusuario direccion)
    {
        dbContext.Update(direccion);
        await dbContext.SaveChangesAsync();
    }


    public async Task Eliminar(Direccionesusuario direccion)
    {
        dbContext.Remove(direccion);
        await dbContext.SaveChangesAsync();
    }

    public async Task<Direccionesusuario> ObtenerDireccionPorId(int idDireccion)
    {
        Direccionesusuario direccionBuscada = await dbContext.Direccionesusuarios.FirstOrDefaultAsync(Direccion => Direccion.IdDireccion == idDireccion);
        return direccionBuscada;
    }

    public async Task<IEnumerable<DireccionDTO>> ObtenerTodasLasDireccionesPorUsuario(Direccionesusuario direccion)
    {
        IEnumerable<DireccionDTO> direccionesDelUsuario = await dbContext.Direccionesusuarios.Include(Direccion => Direccion.IdProvinciaNavigation).Include(Direccion => Direccion.IdCantonNavigation)
            .Include(Direccion => Direccion.IdDistritoNavigation).Where(Direccion => Direccion.IdPersona == direccion.IdPersona).Select(
            direccion => new DireccionDTO
            {
                IdDireccion = direccion.IdDireccion,
                DireccionExacta = direccion.DireccionExacta,
                NombreDireccion = direccion.NombreDireccion,

                ProvinciaDTO = new ProvinciaDTO
                {
                    IdProvincia = direccion.IdProvinciaNavigation.IdProvincia,
                    NombreProvincia = direccion.IdProvinciaNavigation.NombreProvincia
                },
                CantonDTO = new CantonDTO
                {
                    IdCanton = direccion.IdCantonNavigation.IdCanton,
                    NombreCanton = direccion.IdCantonNavigation.NombreCanton
                },
                DistritoDTO = new DistritoDTO
                {
                    IdDistrito = direccion.IdDistritoNavigation.IdDistrito,
                    NombreDistrito = direccion.IdDistritoNavigation.NombreDistrito
                }
            }).ToListAsync();
        return direccionesDelUsuario;
    }

    public async Task<Direccionesusuario> ObtenerDireccionPorNombre(Direccionesusuario direccion)
    {
        Direccionesusuario nombreDireccionRepetido = await dbContext.Direccionesusuarios.FirstOrDefaultAsync(Direccion => Direccion.NombreDireccion == direccion.NombreDireccion && Direccion.IdPersona == direccion.IdPersona && Direccion.IdDireccion != direccion.IdDireccion);
        return nombreDireccionRepetido;
    }

    public async Task<DireccionDTO> ObtenerDireccionPorIdDTO(int idDireccion)
    {
        Direccionesusuario direccionBuscada = await dbContext.Direccionesusuarios.Include(Direccion => Direccion.IdProvinciaNavigation).Include(Direccion => Direccion.IdCantonNavigation)
            .Include(Direccion => Direccion.IdDistritoNavigation).FirstOrDefaultAsync(Direccion => Direccion.IdDireccion == idDireccion);

        DireccionDTO direccionBuscadaDTO = DireccionDTO.ConvertirDireccionADireccionDTO(direccionBuscada);
        return direccionBuscadaDTO;
    }
}
