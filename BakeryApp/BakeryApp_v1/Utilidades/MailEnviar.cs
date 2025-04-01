

using System.Net.Mail;
using System.Net;
using System.Text;
using System.Net.Mime;
using BakeryApp_v1.Services;
using BakeryApp_v1.Models;
using BakeryApp_v1.DTO;
using BakeryApp_v1.DAO;
using BakeryApp_v1.ViewModels;
using Stripe;

namespace BakeryApp_v1.Utilidades;

public class MailEnviar : IMailEnviar
{
    private SmtpClient client;
    private readonly IWebHostEnvironment ambiente;
    private readonly PedidoService pedidoService;
    private readonly DireccionesService direccionesService;

    public MailEnviar(IWebHostEnvironment ambiente, PedidoService pedidoService, DireccionesService direccionesService)
    {
        this.pedidoService = pedidoService;
        this.ambiente = ambiente;
        this.direccionesService = direccionesService;
    }


    public void Configurar()
    {
        client = new SmtpClient("smtp.gmail.com", 587);
        client.EnableSsl = true;
        client.UseDefaultCredentials = false;
        client.Credentials = new NetworkCredential("dulce.espiga2024@gmail.com", "xmmh dnjy zdsm yhnv");
    }

    public async Task<bool> EnviarCorreo(Persona persona, string asunto, string codigoRecuperacion)
    {
        try
        {
            Configurar();
            MailMessage mensaje = new MailMessage();
            mensaje.From = new MailAddress("dulce.espiga2024@gmail.com");
            mensaje.To.Add(persona.Correo);
            mensaje.Subject = asunto;
            mensaje.IsBodyHtml = true;
            StringBuilder mailBody = new StringBuilder();



            mailBody.AppendFormat("<img src='cid:imagenLocal' alt='Imagen Local' style='display: block; margin-left: auto; margin-right: auto;' />");
            mailBody.AppendFormat("<h1 style='text-align: center;'>Código de Recuperación  </h1>");
            mailBody.AppendFormat("<br />");
            mailBody.AppendFormat($"<h2 style='text-align: center;'>{codigoRecuperacion} </h2>");

            mensaje.Body = mailBody.ToString();

            string htmlBody = mailBody.ToString();


            AlternateView htmlAlternativa = AlternateView.CreateAlternateViewFromString(htmlBody, null, MediaTypeNames.Text.Html);

            string imagen = ambiente.WebRootPath;
            imagen = Path.Combine(imagen, "img");
            imagen = Path.Combine(imagen, "Logo_Transparente.png");

            LinkedResource imagenEnviar = new LinkedResource(imagen);

            imagenEnviar.ContentId = "imagenLocal";
            imagenEnviar.ContentType = new ContentType(MediaTypeNames.Image.Png);


            htmlAlternativa.LinkedResources.Add(imagenEnviar);


            mensaje.AlternateViews.Add(htmlAlternativa);





            await client.SendMailAsync(mensaje);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }


    public async Task<bool> EnviarCorreoPedidoConfirmado(Persona persona, string asunto, IEnumerable<CarritoDTO> todosLosElementosDelCarrito, PedidoViewModel pedido)
    {
        try
        {
            Configurar();
            MailMessage mensaje = new MailMessage();
            mensaje.From = new MailAddress("dulce.espiga2024@gmail.com");
            mensaje.To.Add(persona.Correo);
            mensaje.Subject = asunto;
            mensaje.IsBodyHtml = true;
            StringBuilder mailBody = new StringBuilder();


            decimal totalPagar = pedidoService.CalcularTotalPedido(todosLosElementosDelCarrito);

            DireccionDTO direccionUsuario = null;

            // Si el envio es a domicilio
            if (pedido.IdTipoEnvio == 1)
            {
                direccionUsuario = await direccionesService.ObtenerDireccionPorIdDTO(pedido.IdDireccion.Value);
                totalPagar += 2500;
            }
            decimal iva = pedidoService.CalcularIva(totalPagar, pedido);
            decimal totalFinal = totalPagar + iva;

            mailBody.AppendFormat("<img src='cid:imagenLocal' alt='Imagen Local' style='display: block; margin-left: auto; margin-right: auto;' />");
            mailBody.AppendFormat("<h1 style='text-align: center;'>Detalles del pedido</h1>");

            string tipoPago = pedidoService.ObtenerTipoPagoPedido(pedido);



            string tipoEnvio = pedidoService.ObtenerTipoEnvioPedido(pedido);



            mailBody.AppendFormat("<h2 style='text-align:center'>" + "Tipo de pago: " + tipoPago + "</h2>");
            mailBody.AppendFormat("<h2 style='text-align:center'>" + "Tipo de envio: " + tipoEnvio + "</h2>");
            mailBody.AppendFormat("<h2 style='text-align:center'>" + "Total a pagar: " + totalFinal + "₡" + "</h2>");
            if (direccionUsuario is not null)
            {
                mailBody.AppendFormat("<h2 style='text-align:center'>" + "Nombre de la direccion de entrega: " + direccionUsuario.NombreDireccion + "</h2>");
               
                mailBody.AppendFormat("<h2 style='text-align:center'>" + "Dirección Exacta: " + direccionUsuario.ProvinciaDTO.NombreProvincia + ", " + direccionUsuario.CantonDTO.NombreCanton + ", " + direccionUsuario.DistritoDTO.NombreDistrito + "</h2>");
                mailBody.AppendFormat("<h2 style='text-align:center'>" + "Indicaciones adicionales: " + direccionUsuario.DireccionExacta + "</h2>");
            }

            mailBody.AppendFormat("<br />");

            mailBody.AppendFormat("<table style='width:100%; border-collapse:collapse;'>");
            mailBody.AppendFormat("<tr style='background-color:#f2f2f2;'>");
            mailBody.AppendFormat("<th style='border:3px solid #dddddd; padding:8px; text-align:left;'>Producto Adquirido</th>");
            mailBody.AppendFormat("<th style='border:3px solid #dddddd; padding:8px; text-align:left;'>Cantidad Adquirida</th>");
            mailBody.AppendFormat("<th style='border:3px solid #dddddd; padding:8px; text-align:left;'>Precio Unitario </th>");
            mailBody.AppendFormat("</tr>");

            foreach (CarritoDTO elementoCarrito in todosLosElementosDelCarrito)
            {

                mailBody.AppendFormat("<tr>");
                mailBody.AppendFormat("<td style='border:1px solid #dddddd; padding:8px;'> " + elementoCarrito.ProductoDTO.NombreProducto + "</td>");
                mailBody.AppendFormat("<td style='border:1px solid #dddddd; padding:8px;'> " + elementoCarrito.CantidadProducto + "</td>");
                mailBody.AppendFormat("<td style='border:1px solid #dddddd; padding:8px;'> " + elementoCarrito.ProductoDTO.PrecioProducto + "₡" + " </td>");
                mailBody.AppendFormat("</tr>");
            }



          

            mailBody.AppendFormat("</table>");
            mensaje.Body = mailBody.ToString();

            string htmlBody = mailBody.ToString();


            AlternateView htmlAlternativa = AlternateView.CreateAlternateViewFromString(htmlBody, null, MediaTypeNames.Text.Html);

            string imagen = ambiente.WebRootPath;
            imagen = Path.Combine(imagen, "img");
            imagen = Path.Combine(imagen, "Logo_Transparente.png");

            LinkedResource imagenEnviar = new LinkedResource(imagen);

            imagenEnviar.ContentId = "imagenLocal";
            imagenEnviar.ContentType = new ContentType(MediaTypeNames.Image.Png);


            htmlAlternativa.LinkedResources.Add(imagenEnviar);


            mensaje.AlternateViews.Add(htmlAlternativa);





            await client.SendMailAsync(mensaje);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }


    public async Task<bool> EnviarCorreoMarketingPersona(Boletin boletin, Mensajesboletin mensajeBoletin)
    {
        try
        {
            Configurar();
            MailMessage mensaje = new MailMessage();
            mensaje.From = new MailAddress("dulce.espiga2024@gmail.com");
            mensaje.To.Add(boletin.IdUsuarioNavigation.Correo);
            mensaje.Subject = mensajeBoletin.Asunto;
            mensaje.IsBodyHtml = true;
            StringBuilder mailBody = new StringBuilder();


           
       

            mailBody.AppendFormat("<img src='cid:imagenLocal' alt='Imagen Local' style='display: block; margin-left: auto; margin-right: auto;' />");
            mailBody.AppendFormat("<h1 style='text-align: center;'>Mensaje del Boletin de Noticias de BakeryApp</h1>");

            string mensajeBoletinEnviar = mensajeBoletin.Mensaje;

            string nombrePersona = boletin.IdUsuarioNavigation.Nombre;


            mailBody.AppendFormat("<h2 style='text-align:center'>" + "Estimado señor: " + nombrePersona + "</h2>");

            mailBody.AppendFormat("<h2 style='text-align:center'>" + "Mensaje: " + mensajeBoletinEnviar + "</h2>");



            mailBody.AppendFormat("<h2 style='text-align:center'> Gracias por su atención y preferencia </h2>");

            mensaje.Body = mailBody.ToString();

            string htmlBody = mailBody.ToString();


            AlternateView htmlAlternativa = AlternateView.CreateAlternateViewFromString(htmlBody, null, MediaTypeNames.Text.Html);

            string imagen = ambiente.WebRootPath;
            imagen = Path.Combine(imagen, "img");
            imagen = Path.Combine(imagen, "Logo_Transparente.png");

            LinkedResource imagenEnviar = new LinkedResource(imagen);

            imagenEnviar.ContentId = "imagenLocal";
            imagenEnviar.ContentType = new ContentType(MediaTypeNames.Image.Png);


            htmlAlternativa.LinkedResources.Add(imagenEnviar);


            mensaje.AlternateViews.Add(htmlAlternativa);





            await client.SendMailAsync(mensaje);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }

    }

    public async Task<bool> EnviarCorreoMarketingTodos(IEnumerable<Boletin> todosLosBoletinesUsuario, Mensajesboletin mensajeBoletin)
    {
        try
        {
            Configurar();

            foreach (Boletin boletin in todosLosBoletinesUsuario)
            {

                MailMessage mensaje = new MailMessage();
                mensaje.From = new MailAddress("dulce.espiga2024@gmail.com");
                mensaje.To.Add(boletin.IdUsuarioNavigation.Correo);
                mensaje.Subject = mensajeBoletin.Asunto;
                mensaje.IsBodyHtml = true;
                StringBuilder mailBody = new StringBuilder();





                mailBody.AppendFormat("<img src='cid:imagenLocal' alt='Imagen Local' style='display: block; margin-left: auto; margin-right: auto;' />");
                mailBody.AppendFormat("<h1 style='text-align: center;'>Mensaje del Boletin de Noticias de BakeryApp</h1>");

                string mensajeBoletinEnviar = mensajeBoletin.Mensaje;

                string nombrePersona = boletin.IdUsuarioNavigation.Nombre;


                mailBody.AppendFormat("<h2 style='text-align:center'>" + "Estimado señor: " + nombrePersona + "</h2>");

                mailBody.AppendFormat("<h2 style='text-align:center'>" + "Mensaje: " + mensajeBoletinEnviar + "</h2>");



                mailBody.AppendFormat("<h2 style='text-align:center'> Gracias por su atención y preferencia </h2>");

                mensaje.Body = mailBody.ToString();

                string htmlBody = mailBody.ToString();


                AlternateView htmlAlternativa = AlternateView.CreateAlternateViewFromString(htmlBody, null, MediaTypeNames.Text.Html);

                string imagen = ambiente.WebRootPath;
                imagen = Path.Combine(imagen, "img");
                imagen = Path.Combine(imagen, "Logo_Transparente.png");

                LinkedResource imagenEnviar = new LinkedResource(imagen);

                imagenEnviar.ContentId = "imagenLocal";
                imagenEnviar.ContentType = new ContentType(MediaTypeNames.Image.Png);


                htmlAlternativa.LinkedResources.Add(imagenEnviar);


                mensaje.AlternateViews.Add(htmlAlternativa);





                await client.SendMailAsync(mensaje);
            }
            return true;

        }
        catch (Exception ex)
        {
            return false;
        }

    }

}
