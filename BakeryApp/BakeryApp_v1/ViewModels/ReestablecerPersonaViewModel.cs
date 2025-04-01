using BakeryApp_v1.Models;

namespace BakeryApp_v1.ViewModels;

public class ReestablecerPersonaViewModel
{
    public int IdPersona { get; set; }

    public string CodigoRecuperacion { get; set; } = null!;

    public string Correo { get; set; } = null!;
    public string Contra { get; set; } = null!;
    public DateTime FechaExpiracion { get; set; }


    public static Persona ConvertirReestablecerPersonaViewModelAPersona(ReestablecerPersonaViewModel persona)
    {
        return new Persona
        {

            IdPersona = persona.IdPersona,
            Correo = persona.Correo,
            Contra = persona.Contra
        };
    }


}
