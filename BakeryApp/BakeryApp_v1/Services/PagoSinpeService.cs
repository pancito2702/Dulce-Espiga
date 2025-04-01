using BakeryApp_v1.Models;
using BakeryApp_v1.ViewModels;

namespace BakeryApp_v1.Services;

public interface PagoSinpeService
{
    public Task GuardarPagoSinpe(Pagossinpe pagoSinpe);

    public Task EditarPagoSinpe(Pagossinpe pagoSinpe);

    public Task<IEnumerable<Pagossinpe>> ObtenerTodosLosPagosSinpe();

    public Task<Pagossinpe> ObtenerPagoSinpePorPedido(int idPedido);

    public bool VerificarPagoSinpeVacio(PagoSinpeViewModel pago);
}
