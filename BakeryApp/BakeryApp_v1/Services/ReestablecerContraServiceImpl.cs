using BakeryApp_v1.DAO;
using BakeryApp_v1.Models;

namespace BakeryApp_v1.Services;

public class ReestablecerContraServiceImpl : ReestablecerContraService
{
    private readonly ReestablecerContraDAO reestablecerContraDAO;

    public ReestablecerContraServiceImpl(ReestablecerContraDAO reestablecerContraDAO)
    {
        this.reestablecerContraDAO = reestablecerContraDAO;
    }

    public async Task Guardar(Recuperarcontra persona)
    {
        await reestablecerContraDAO.Guardar(persona);
    }

    public async Task Eliminar(Recuperarcontra persona)
    {
        await reestablecerContraDAO.Eliminar(persona);
    }

    public async Task<Recuperarcontra> ObtenerPorIdPersona(Recuperarcontra persona)
    {
        Recuperarcontra personaContra = await reestablecerContraDAO.ObtenerPorIdPersona(persona);
        return personaContra;
    }


    public async Task<bool> VerificarFechaCodigo(Recuperarcontra persona)
    {
        Recuperarcontra personaContra = await reestablecerContraDAO.ObtenerPorIdPersona(persona);

        // Si la persona no es null
        if (personaContra is not null)
        {
            if (DateTime.Now > personaContra.FechaExpiracion)
            {
                return true;
            }
        }

        return false;

    }

    public Recuperarcontra ConvertirPersonaARecuperarPersona(Persona persona, string codigoRecuperacion)
    {
        return new Recuperarcontra
        {
            IdPersona = persona.IdPersona,
            CodigoRecuperacion = codigoRecuperacion,
            FechaExpiracion = DateTime.Now.AddMinutes(5)
        };
    }

    public Recuperarcontra ConvertirPersonaARecuperarPersona(Persona persona)
    {
        return new Recuperarcontra
        {
            IdPersona = persona.IdPersona
        };
    }

   

    public async Task<int> ObtenerCantidadCodigosPersona(Recuperarcontra persona)
    {
        int cantidadCodigos = await reestablecerContraDAO.ObtenerCantidadCodigosPersona(persona);
        return cantidadCodigos;
    }


    public bool EsVacioCodigo(Recuperarcontra persona)
    {
        if (string.IsNullOrEmpty(persona.CodigoRecuperacion))
        {
            return true;
        }

        return false;

    }

}
