using BakeryApp_v1.Models;

namespace BakeryApp_v1.Services;

public interface ReestablecerContraService
{
    public Task Guardar(Recuperarcontra persona);

    public Task Eliminar(Recuperarcontra persona);


    public Task<Recuperarcontra> ObtenerPorIdPersona(Recuperarcontra persona);

    public Task<bool> VerificarFechaCodigo(Recuperarcontra persona);

    public Recuperarcontra ConvertirPersonaARecuperarPersona(Persona persona, string codigoRecuperacion);

    public Recuperarcontra ConvertirPersonaARecuperarPersona(Persona persona);
    public Task<int> ObtenerCantidadCodigosPersona(Recuperarcontra persona);

    public bool EsVacioCodigo(Recuperarcontra persona);
}
