using BakeryApp_v1.DTO;
using BakeryApp_v1.Models;
namespace BakeryApp_v1.DAO;

public interface IngredienteDAO
{
    public Task Guardar(Ingrediente ingrediente);

    public Task Editar(Ingrediente ingrediente);


    public Task Eliminar(Ingrediente ingrediente);



    public Task<IngredienteDTO> ObtenerIngredienteDTOPorId(int idIngrediente);

    public Task<Ingrediente> ObtenerIngredientePorId(int idIngrediente);
    public Task<IEnumerable<IngredienteDTO>> ObtenerTodasLasIngredientes(int pagina);

    public Task<IEnumerable<Ingrediente>> ObtenerTodasLasIngredientes();

    public void IngredientesAttach(IEnumerable<Ingrediente> ingredientes);


    public Task<Ingrediente> ObtenerIngredientePorNombre(Ingrediente ingrediente);

    public Task<int> ContarTotalIngredientes();

}
