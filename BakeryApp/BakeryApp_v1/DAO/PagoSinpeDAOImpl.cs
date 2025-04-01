using BakeryApp_v1.Models;
using Microsoft.EntityFrameworkCore;
namespace BakeryApp_v1.DAO;

public class PagoSinpeDAOImpl : PagoSinpeDAO
{
    private readonly BakeryAppContext dbContext;

    public PagoSinpeDAOImpl(BakeryAppContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task EditarPagoSinpe(Pagossinpe pagoSinpe)
    {
        dbContext.Update(pagoSinpe);
        await dbContext.SaveChangesAsync();
    }

    public async Task GuardarPagoSinpe(Pagossinpe pagoSinpe)
    {
        dbContext.Add(pagoSinpe);
        await dbContext.SaveChangesAsync();

    }

    public async Task<Pagossinpe> ObtenerPagoSinpePorPedido(int idPedido)
    {
        Pagossinpe pagoSinpeBuscado = await dbContext.Pagossinpes.FirstOrDefaultAsync(Pago => Pago.IdPedido == idPedido); 
        return pagoSinpeBuscado;
    }

    public async Task<IEnumerable<Pagossinpe>> ObtenerTodosLosPagosSinpe()
    {
        IEnumerable<Pagossinpe> todosLosPagosSinpe = await dbContext.Pagossinpes.ToListAsync();
        return todosLosPagosSinpe;
    }
}
