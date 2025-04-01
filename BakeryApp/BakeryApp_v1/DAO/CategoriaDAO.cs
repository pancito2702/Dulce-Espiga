using BakeryApp_v1.Models;

namespace BakeryApp_v1.DAO;

public interface CategoriaDAO
{
    public Task Guardar(Categoria categoria);

    public Task Editar(Categoria categoria);


    public Task Eliminar(Categoria categoria);

    public Task<Categoria> ObtenerCategoriaEspecifica(Categoria categoria);

    public Task<Categoria> ObtenerCategoriaPorId(int idCategoria);

    public Task<IEnumerable<Categoria>> ObtenerTodasLasCategorias(int pagina);

    public Task<IEnumerable<Categoria>> ObtenerTodasLasCategorias();

    public Task<Categoria> ObtenerCategoriaPorNombre(Categoria categoria);

    public Task<int> ContarTotalCategorias();

}
