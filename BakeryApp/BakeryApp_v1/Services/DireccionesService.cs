using BakeryApp_v1.DTO;
using BakeryApp_v1.Models;

namespace BakeryApp_v1.Services;

public interface DireccionesService
{
    public Task Guardar(Direccionesusuario direccion);

    public Task Editar(Direccionesusuario direccion);


    public Task Eliminar(Direccionesusuario direccion);

    public Task<Direccionesusuario> ObtenerDireccionPorId(int idDireccion);

    public Task<IEnumerable<DireccionDTO>> ObtenerTodasLasDireccionesPorUsuario(Direccionesusuario direccion);

    public Task<bool> ObtenerDireccionPorNombre(Direccionesusuario direccion);

    public bool DatosVaciosONulos(Direccionesusuario direccion);

    public bool VerificarIdValidoDireccion(int idDireccion);

    public bool VerificarDireccionPerteneceUsuario(Direccionesusuario direccionAEliminar, Persona personaLogueada);

    public Task<DireccionDTO> ObtenerDireccionPorIdDTO(int idDireccion);


}
