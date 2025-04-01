using BakeryApp_v1.Models;

namespace BakeryApp_v1.ViewModels
{
    public class PagoSinpeViewModel
    {
        public int IdPagoSinpe { get; set; }

        public int IdPedido { get; set; }

        public string RutaImagenSinpe { get; set; } = null!;

        public IFormFile ArchivoSinpe { get; set; }


        public static Pagossinpe ConvertirPagoSinpeViewModelAPagoSinpe(PagoSinpeViewModel pago)
        {
            return new Pagossinpe
            {
                IdPagoSinpe = pago.IdPagoSinpe,
                IdPedido = pago.IdPedido,
                RutaImagenSinpe = pago.RutaImagenSinpe,
                ArchivoSinpe = pago.ArchivoSinpe
            };
        }
    }
}
