using BakeryApp_v1.DAO;
using BakeryApp_v1.Models;

namespace BakeryApp_v1.Services;

public class CategoriaServiceImpl : CategoriaService
{
    private readonly CategoriaDAO categoriaDAO;

    public CategoriaServiceImpl(CategoriaDAO categoriaDAO)
    {
        this.categoriaDAO = categoriaDAO;
    }

    public async Task Guardar(Categoria categoria)
    {
        await categoriaDAO.Guardar(categoria);
    }

    public async Task Editar(Categoria categoria)
    {
        await categoriaDAO.Editar(categoria);
    }

    public async Task Eliminar(Categoria categoria)
    {
        await categoriaDAO.Eliminar(categoria);
    }

    public async Task<IEnumerable<Categoria>> ObtenerTodasLasCategorias()
    {
        IEnumerable<Categoria> todasLasCategorias = await categoriaDAO.ObtenerTodasLasCategorias();
        return todasLasCategorias;
    }

    public async Task<Categoria> ObtenerCategoriaEspecifica(Categoria categoria)
    {
        Categoria categoriaBuscada = await categoriaDAO.ObtenerCategoriaEspecifica(categoria);
        return categoriaBuscada;
    }

    public async Task<Categoria> ObtenerCategoriaPorId(int idCategoria)
    {
        Categoria categoriaBuscada = await categoriaDAO.ObtenerCategoriaPorId(idCategoria);
        return categoriaBuscada;
    }

    public async Task<IEnumerable<Categoria>> ObtenerTodasLasCategorias(int pagina)
    {
        IEnumerable<Categoria> todasLasCategorias = await categoriaDAO.ObtenerTodasLasCategorias(pagina);
        return todasLasCategorias;
    }

    public bool VerificarDatosVaciosONulos(Categoria categoria)
    {
        if (string.IsNullOrEmpty(categoria.NombreCategoria) || (string.IsNullOrEmpty(categoria.DescripcionCategoria)) || categoria.ArchivoCategoria == null)
        {
            return true;
        }



        return false;
    }


    public async Task<bool> VerificarNombreRepetido(Categoria categoria)
    {
        Categoria estaRepetida = await categoriaDAO.ObtenerCategoriaPorNombre(categoria);

        if (estaRepetida == null)
        {
            return false;
        }

        return true;
    }

    public async Task<int> CalcularTotalPaginas()
    {
        int totalCategorias = await categoriaDAO.ContarTotalCategorias();
        int elementosPorPagina = 9;
        double totalPaginas = (double)totalCategorias / elementosPorPagina;
        totalPaginas = Math.Ceiling(totalPaginas);

        return (int)totalPaginas;
    }
}
