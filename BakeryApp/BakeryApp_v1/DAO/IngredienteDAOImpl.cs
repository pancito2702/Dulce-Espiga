using BakeryApp_v1.DTO;
using BakeryApp_v1.Models;
using Microsoft.EntityFrameworkCore;
using PagedList;

namespace BakeryApp_v1.DAO;

public class IngredienteDAOImpl : IngredienteDAO
{
    private readonly BakeryAppContext dbContext;

    public IngredienteDAOImpl(BakeryAppContext dbContext)
    {
        this.dbContext = dbContext;
    }


    public async Task Guardar(Ingrediente ingrediente)
    {
        dbContext.Add(ingrediente);
        await dbContext.SaveChangesAsync();
    }

    public async Task Editar(Ingrediente ingrediente)
    {
        dbContext.Update(ingrediente);
        await dbContext.SaveChangesAsync();
    }


    public async Task Eliminar(Ingrediente ingrediente)
    {
        dbContext.Remove(ingrediente);
        await dbContext.SaveChangesAsync();
    }



    public async Task<Ingrediente> ObtenerIngredientePorId(int idIngrediente)
    {
        Ingrediente ingredienteEncontrado = await dbContext.Ingredientes.FirstOrDefaultAsync(Ingrediente => Ingrediente.IdIngrediente == idIngrediente);

        return ingredienteEncontrado;
    }


    public async Task<IngredienteDTO> ObtenerIngredienteDTOPorId(int idIngrediente)
    {
        Ingrediente ingredienteEncontrado = await dbContext.Ingredientes.FirstOrDefaultAsync(Ingrediente => Ingrediente.IdIngrediente == idIngrediente);

        return IngredienteDTO.ConvertirIngredienteAIngredienteDTO(ingredienteEncontrado);
    }
    public async Task<IEnumerable<IngredienteDTO>> ObtenerTodasLasIngredientes(int pagina)
    {
        int numeroDeElementosPorPagina = 10;

        IPagedList<IngredienteDTO> todasLasIngredientes = dbContext.Ingredientes.OrderBy(Ingrediente => Ingrediente.IdIngrediente).Include(Ingrediente => Ingrediente.UnidadMedidaIngredienteNavigation)
            .Select(Ingrediente => new IngredienteDTO
            {
                IdIngrediente = Ingrediente.IdIngrediente,
                NombreIngrediente = Ingrediente.NombreIngrediente,
                DescripcionIngrediente = Ingrediente.DescripcionIngrediente,
                CantidadIngrediente = Ingrediente.CantidadIngrediente,
                PrecioUnidadIngrediente = Ingrediente.PrecioUnidadIngrediente,
                FechaCaducidadIngrediente = Ingrediente.FechaCaducidadIngrediente.ToString("dd/MM/yyyy"),
                UnidadMedidaIngrediente = Ingrediente.UnidadMedidaIngrediente,
                UnidadMedidaDTO = new UnidadMedidaDTO
                {
                    IdUnidad = Ingrediente.UnidadMedidaIngredienteNavigation.IdUnidad,
                    NombreUnidad = Ingrediente.UnidadMedidaIngredienteNavigation.NombreUnidad
                }
            })
            .ToPagedList(pageNumber: pagina, pageSize: numeroDeElementosPorPagina);
        return todasLasIngredientes;
    }

    public async Task<IEnumerable<Ingrediente>> ObtenerTodasLasIngredientes()
    {
        IEnumerable<Ingrediente> todosLosIngredientes = await dbContext.Ingredientes.ToListAsync();

        return todosLosIngredientes;
    }


    public async Task<Ingrediente> ObtenerIngredientePorNombre(Ingrediente ingrediente)
    {
        Ingrediente ingredienteEncontrado = await dbContext.Ingredientes.FirstOrDefaultAsync(Ingrediente => Ingrediente.NombreIngrediente == ingrediente.NombreIngrediente && Ingrediente.IdIngrediente != ingrediente.IdIngrediente);
        return ingredienteEncontrado;
    }

    public async Task<int> ContarTotalIngredientes()
    {
        int totalIngredientes = await dbContext.Ingredientes.CountAsync();
        return totalIngredientes;
    }

    public void IngredientesAttach(IEnumerable<Ingrediente> ingredientes)
    {
        foreach (Ingrediente ingrediente in ingredientes)
        {
            dbContext.Attach(ingrediente);
        }
    }


}
