using BakeryApp_v1.DAO;
using BakeryApp_v1.Models;
using BakeryApp_v1.ViewModels;

namespace BakeryApp_v1.Services;

public class PagoSinpeServiceImpl : PagoSinpeService
{
    private readonly PagoSinpeDAO pagoSinpeDAO;

    public PagoSinpeServiceImpl(PagoSinpeDAO pagoSinpeDao)
    {
        this.pagoSinpeDAO = pagoSinpeDao;
    }

    public async Task EditarPagoSinpe(Pagossinpe pagoSinpe)
    {
       await pagoSinpeDAO.EditarPagoSinpe(pagoSinpe);
    }

    public async Task GuardarPagoSinpe(Pagossinpe pagoSinpe)
    {
       await pagoSinpeDAO.GuardarPagoSinpe(pagoSinpe);
    }

    public async Task<Pagossinpe> ObtenerPagoSinpePorPedido(int idPedido)
    {
        Pagossinpe pagosSinpeBuscado = await pagoSinpeDAO.ObtenerPagoSinpePorPedido(idPedido);   
        return pagosSinpeBuscado;
    }

    public async Task<IEnumerable<Pagossinpe>> ObtenerTodosLosPagosSinpe()
    {
        IEnumerable<Pagossinpe> todosLosPagosSinpe = await pagoSinpeDAO.ObtenerTodosLosPagosSinpe();    
        return todosLosPagosSinpe;
    }

    public bool VerificarPagoSinpeVacio(PagoSinpeViewModel pago)
    {
        if (pago == null)
        {
            return false;
        }
        return true;
    }
}
