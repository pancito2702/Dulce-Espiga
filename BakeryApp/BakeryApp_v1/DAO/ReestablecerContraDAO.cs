using BakeryApp_v1.Models;

namespace BakeryApp_v1.DAO;

public interface ReestablecerContraDAO
{
    public Task Guardar(Recuperarcontra persona);

    public Task Eliminar(Recuperarcontra persona);

    public Task<Recuperarcontra> ObtenerPorIdPersona(Recuperarcontra persona);

    public Task<int> ObtenerCantidadCodigosPersona(Recuperarcontra persona);

}
