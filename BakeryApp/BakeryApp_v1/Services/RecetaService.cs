using BakeryApp_v1.DTO;
using BakeryApp_v1.Models;
namespace BakeryApp_v1.Services;

public interface RecetaService
{
    public Task Guardar(Receta receta);

    public Task Editar(Receta receta);
    public Task Eliminar(Receta receta);

    public Task<Receta> ObtenerRecetaPorId(int idReceta);
    public Task<IEnumerable<RecetaDTO>> ObtenerTodasLasRecetas(int pagina);

    public Task<IEnumerable<Receta>> ObtenerTodasLasRecetas();

    public bool VerificarDatosVaciosONulos(Receta receta);


    public Task<RecetaDTO> ObtenerRecetaPorIdDTO(int idReceta);
    public Task<bool> VerificarNombreRepetido(Receta receta);

    public Task<int> CalcularTotalPaginas();

    public Task LimpiarReceta(Receta receta);
}
