using BakeryApp_v1.Models;
using Microsoft.EntityFrameworkCore;
using PagedList;

namespace BakeryApp_v1.DAO;

public class CategoriaDAOImpl : CategoriaDAO
{
    private readonly BakeryAppContext dbContext;

    public CategoriaDAOImpl(BakeryAppContext dbContext)
    {
        this.dbContext = dbContext;
    }


    public async Task Guardar(Categoria categoria)
    {
        dbContext.Add(categoria);
        await dbContext.SaveChangesAsync();
    }

    public async Task Editar(Categoria categoria)
    {
        dbContext.Update(categoria);
        await dbContext.SaveChangesAsync();
    }


    public async Task Eliminar(Categoria categoria)
    {
        dbContext.Remove(categoria);
        await dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Categoria>> ObtenerTodasLasCategorias()
    {
        IEnumerable<Categoria> todasLasCategorias = await dbContext.Categorias.ToListAsync();
        return todasLasCategorias;
    }

    public async Task<Categoria> ObtenerCategoriaEspecifica(Categoria categoria)
    {
        
        Categoria categoriaEncontrada = await dbContext.Categorias.FirstOrDefaultAsync(Categoria => Categoria.IdCategoria == categoria.IdCategoria);
        return categoriaEncontrada;
    }

    public async Task<Categoria> ObtenerCategoriaPorId(int idCategoria)
    {
        Categoria categoriaEncontrada = await dbContext.Categorias.FirstOrDefaultAsync(Categoria => Categoria.IdCategoria == idCategoria);
        return categoriaEncontrada;
    }

    public async Task<IEnumerable<Categoria>> ObtenerTodasLasCategorias(int pagina)
    {
        int numeroDeElementosPorPagina = 9;

        IPagedList<Categoria> todasLasCategorias = dbContext.Categorias.OrderBy(Categoria => Categoria.IdCategoria).ToPagedList(pageNumber: pagina, pageSize: numeroDeElementosPorPagina);
        return todasLasCategorias;
    }

    public async Task<Categoria> ObtenerCategoriaPorNombre(Categoria categoria)
    {
        Categoria categoriaEncontrada = await dbContext.Categorias.FirstOrDefaultAsync(Categoria => Categoria.NombreCategoria == categoria.NombreCategoria && Categoria.IdCategoria != categoria.IdCategoria);
        return categoriaEncontrada;
    }

    public async Task<int> ContarTotalCategorias()
    {
        int totalCategorias = await dbContext.Categorias.CountAsync();
        return totalCategorias;
    }

}
