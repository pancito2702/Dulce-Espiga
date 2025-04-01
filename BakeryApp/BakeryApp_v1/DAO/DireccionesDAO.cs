using BakeryApp_v1.DTO;
using BakeryApp_v1.Models;

namespace BakeryApp_v1.DAO;

public interface DireccionesDAO
{
    public Task Guardar(Direccionesusuario direccion);

    public Task Editar(Direccionesusuario direccion);


    public Task Eliminar(Direccionesusuario direccion);

    public Task<Direccionesusuario> ObtenerDireccionPorId(int idDireccion);

    public Task<IEnumerable<DireccionDTO>> ObtenerTodasLasDireccionesPorUsuario(Direccionesusuario direccion);

    public Task<Direccionesusuario> ObtenerDireccionPorNombre(Direccionesusuario direccion);

    public Task<DireccionDTO> ObtenerDireccionPorIdDTO(int idDireccion);

}
