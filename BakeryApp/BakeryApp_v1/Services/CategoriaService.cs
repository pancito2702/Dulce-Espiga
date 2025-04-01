using BakeryApp_v1.Models;

namespace BakeryApp_v1.Services;

public interface CategoriaService
{
    public Task Guardar(Categoria categoria);

    public Task Editar(Categoria categoria);
    public Task Eliminar(Categoria categoria);

    public Task<Categoria> ObtenerCategoriaEspecifica(Categoria categoria);

    public Task<Categoria> ObtenerCategoriaPorId(int idCategoria);

    public Task<IEnumerable<Categoria>> ObtenerTodasLasCategorias(int pagina);

    public Task<IEnumerable<Categoria>> ObtenerTodasLasCategorias();
    public bool VerificarDatosVaciosONulos(Categoria categoria);

    public Task<bool> VerificarNombreRepetido(Categoria categoria);

    public Task<int> CalcularTotalPaginas();
}
