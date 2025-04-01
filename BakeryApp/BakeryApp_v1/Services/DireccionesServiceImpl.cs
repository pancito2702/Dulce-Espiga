using BakeryApp_v1.DAO;
using BakeryApp_v1.DTO;
using BakeryApp_v1.Models;

namespace BakeryApp_v1.Services;

public class DireccionesServiceImpl : DireccionesService
{
    private readonly DireccionesDAO direccionesDAO;

    public DireccionesServiceImpl(DireccionesDAO direccionesDAO)
    {
        this.direccionesDAO = direccionesDAO;
    }

    public async Task Editar(Direccionesusuario direccion)
    {
        await direccionesDAO.Editar(direccion);
    }

    public async Task Eliminar(Direccionesusuario direccion)
    {
        await direccionesDAO.Eliminar(direccion);
    }

    public async Task Guardar(Direccionesusuario direccion)
    {
        await direccionesDAO.Guardar(direccion);    
    }

    public async Task<Direccionesusuario> ObtenerDireccionPorId(int idDireccion)
    {
        Direccionesusuario direccionEncontrada = await direccionesDAO.ObtenerDireccionPorId(idDireccion);
        return direccionEncontrada;
    }

    public async Task<bool> ObtenerDireccionPorNombre(Direccionesusuario direccion)
    {
        Direccionesusuario direccionNombreRepetido = await direccionesDAO.ObtenerDireccionPorNombre(direccion);

        if (direccionNombreRepetido == null)
        {
            return false;
        }

        return true;
    }

    public async Task<IEnumerable<DireccionDTO>> ObtenerTodasLasDireccionesPorUsuario(Direccionesusuario direccion)
    {
        IEnumerable<DireccionDTO> todasLasDireccionesDelUsuario = await direccionesDAO.ObtenerTodasLasDireccionesPorUsuario(direccion);
        return todasLasDireccionesDelUsuario;
    }

    public bool DatosVaciosONulos(Direccionesusuario direccion)
    {
        if (string.IsNullOrEmpty(direccion.NombreDireccion) || string.IsNullOrEmpty(direccion.DireccionExacta) || direccion.IdCanton == 0 || direccion.IdProvincia == 0 || direccion.IdDistrito == 0 || direccion.IdProvincia == 0)
        {
            return true;
        }
        return false;
    }

    public bool VerificarIdValidoDireccion(int idDireccion)
    {
        if (idDireccion == 0 || idDireccion < 0)
        {
            return false;
        }

        return true;
    }


    public bool VerificarDireccionPerteneceUsuario(Direccionesusuario direccionAEliminar, Persona personaLogueada)
    {
        if (direccionAEliminar.IdPersona ==  personaLogueada.IdPersona)
        {
            return true;
        }
        return false;
    }

    public async Task<DireccionDTO> ObtenerDireccionPorIdDTO(int idDireccion)
    {
        DireccionDTO direccionBuscada = await direccionesDAO.ObtenerDireccionPorIdDTO(idDireccion);
        return direccionBuscada;
    }
}
