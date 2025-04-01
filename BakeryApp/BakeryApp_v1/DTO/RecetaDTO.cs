using BakeryApp_v1.Models;

namespace BakeryApp_v1.DTO;

public class RecetaDTO
{
    public int IdReceta { get; set; }

    public string NombreReceta { get; set; } = null!;

    public string Instrucciones { get; set; } = null!;


    public List<IngredienteDTO> Ingredientes { get; set; } = new List<IngredienteDTO>();

    public static RecetaDTO ConvertirRecetaARecetaDTO(Receta receta)
    {

        if (receta == null)
        {
            return null;
        }

        return new RecetaDTO
        {
            IdReceta = receta.IdReceta,
            NombreReceta = receta.NombreReceta,
            Instrucciones = receta.Instrucciones,
            Ingredientes = receta.IdIngredientes.Select(
                ingrediente => new IngredienteDTO
                {
                    NombreIngrediente = ingrediente.NombreIngrediente,
                }
                ).ToList(),
        };
    }


}
