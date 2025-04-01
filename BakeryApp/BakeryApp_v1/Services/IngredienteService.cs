using BakeryApp_v1.DTO;
using BakeryApp_v1.Models;
namespace BakeryApp_v1.Services;

public interface IngredienteService
{
    public Task Guardar(Ingrediente ingrediente);

    public Task Editar(Ingrediente ingrediente);
    public Task Eliminar(Ingrediente ingrediente);

    public Task<IngredienteDTO> ObtenerIngredienteDTOPorId(int idIngrediente);

    public Task<Ingrediente> ObtenerIngredientePorId(int idIngrediente);
    public Task<IEnumerable<IngredienteDTO>> ObtenerTodasLasIngredientes(int pagina);

    public Task<IEnumerable<Ingrediente>> ObtenerTodasLasIngredientes();

    public bool VerificarDatosVaciosONulos(Ingrediente ingrediente);

    public Task<bool> VerificarNombreRepetido(Ingrediente ingrediente);

    public Task<bool> VerificarIngredienteConReceta(int idIngrediente);    
    public bool VerificarFechaVencimiento(Ingrediente ingrediente);

    public bool VerificarPrecioPositivo(Ingrediente ingrediente);

    public bool VerificarCantidadPositiva(Ingrediente ingrediente);

    public void AttachIngredientes(IEnumerable<Ingrediente> ingredientes);
    public Task<int> CalcularTotalPaginas();
}
