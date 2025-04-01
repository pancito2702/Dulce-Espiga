using BakeryApp_v1.DAO;
using BakeryApp_v1.DTO;
using BakeryApp_v1.Models;
using BakeryApp_v1.Services;
using BakeryApp_v1.Utilidades;
using BakeryApp_v1.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using System.Security.Claims;


namespace BakeryApp_v1.Controllers
{
    [Authorize(Policy = "SoloClientes")]
    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
    public class PedidoController : Controller
    {
        private readonly TiposDeEnvioService tiposDeEnvioService;
        private readonly TiposDePagoService tiposDePagoService;
        private readonly PedidoService pedidoService;
        private readonly DireccionesService direccionesService;
        private readonly PersonaService personaService;
        private readonly IFuncionesUtiles funcionesUtiles;
        private readonly PagoSinpeService pagoSinpeService;
        private readonly CarritoService carritoService;
        private readonly ProductoPedidoService productoPedidoService;
        private readonly IMailEnviar correoService;
        private readonly FacturaService facturaService;

        public PedidoController(TiposDeEnvioService tiposDeEnvioService, TiposDePagoService tiposDePagoService, PedidoService pedidoService, DireccionesService direccionesService,
            PersonaService personaService, IFuncionesUtiles funcionesUtiles,
            PagoSinpeService pagoSinpeService, CarritoService carritoService,
            ProductoPedidoService productoPedidoService,
            IMailEnviar correoService, FacturaService facturaService)
        {
            this.tiposDeEnvioService = tiposDeEnvioService;
            this.tiposDePagoService = tiposDePagoService;
            this.pedidoService = pedidoService;
            this.direccionesService = direccionesService;
            this.personaService = personaService;
            this.funcionesUtiles = funcionesUtiles;
            this.pagoSinpeService = pagoSinpeService;
            this.carritoService = carritoService;
            this.productoPedidoService = productoPedidoService;
            this.correoService = correoService;
            this.facturaService = facturaService;
        }

        public IActionResult ErrorPago()
        {
            return View();
        }
        public async  Task<IActionResult> Checkout()
        {
            string correoUsuario = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;

            Persona personaABuscar = new Persona
            {
                Correo = correoUsuario
            };

            personaABuscar = await personaService.ObtenerPersonaPorCorreo(personaABuscar);


            IEnumerable<CarritoDTO> todosLosElementosDelCarrito = await carritoService.ObtenerCarritoUsuario(personaABuscar.IdPersona);


            if (todosLosElementosDelCarrito.Count() == 0)
            {
                return BadRequest();
            }





            return View();
        }

        public IActionResult Gracias([FromQuery] string? checkout)
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> PagoCompletado()
        {

            const string endpointSecret = "whsec_ad9ef5df2c597c92d4d3a67be0f12b9293c49906087e28d321739855de8f7d3e";

            string json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            try
            {
                Event eventoStripe = EventUtility.ConstructEvent(json,
                    Request.Headers["Stripe-Signature"], endpointSecret);


                if (eventoStripe.Type == Events.CheckoutSessionCompleted)
                {


                    Session intentoPago = eventoStripe.Data.Object as Session;




                    string correoUsuario = intentoPago.CustomerEmail;
                    Persona personaABuscar = new Persona
                    {
                        Correo = correoUsuario
                    };

                    personaABuscar = await personaService.ObtenerPersonaPorCorreo(personaABuscar);

                    IEnumerable<CarritoDTO> todosLosElementosDelCarrito = await carritoService.ObtenerCarritoUsuario(personaABuscar.IdPersona);



                    Pedido pedidoObjeto = new Pedido();

                    pedidoObjeto.IdPersona = personaABuscar.IdPersona;
                 


                    pedidoObjeto.IdTipoEnvio = int.Parse(intentoPago.Metadata["IdTipoEnvio"]);


                    if (!string.IsNullOrEmpty(intentoPago.Metadata["IdDireccion"]))
                    {
                           pedidoObjeto.IdDireccion = int.Parse(intentoPago.Metadata["IdDireccion"]);
                    }

                    //Tipo de pago en tarjeta
                    pedidoObjeto.IdTipoPago = 3;


                    // Se asigna el id de estado pedido como 4 (Pagado)
                    pedidoObjeto.IdEstadoPedido = 4;

                    //Se establece la fecha del pedido como la fecha actual
                    pedidoObjeto.FechaPedido = DateTime.Now;

                    await pedidoService.Guardar(pedidoObjeto);

                    bool correoEnviado = await correoService.EnviarCorreoPedidoConfirmado(personaABuscar, "Pedido confirmado", todosLosElementosDelCarrito, PedidoViewModel.ConvertirPedidoAPedidoViewModel(pedidoObjeto));


                    // Se insertan los elementos del carrito en los productos del pedido
                    foreach (CarritoDTO elementoCarrito in todosLosElementosDelCarrito)
                    {
                        Pedidoproducto productoPedido = CarritoDTO.ConvertirCarritoAProductoPedido(elementoCarrito, pedidoObjeto.IdPedido);
                        await productoPedidoService.Guardar(productoPedido);
                    }

                    // Se eliminan los elementos del carrito 
                    foreach (CarritoDTO elementoCarrito in todosLosElementosDelCarrito)
                    {
                        Carritocompra carritoNormal = CarritoDTO.ConvertirCarritoDTOACarrito(elementoCarrito);
                        await carritoService.Eliminar(carritoNormal);
                    }


                    // Se guarda la factura
                    bool resultadoGuardarFactura = await facturaService.GuardarFactura(pedidoObjeto);
                }
              
               

                return Ok();
            }
            catch (StripeException e)
            {
                return BadRequest();
            }
        }

        public async Task<IActionResult> VerFactura([FromQuery] int idPedido)
        {
            Factura facturaABuscar = await facturaService.ObtenerPorIdPedido(idPedido); 

            if (facturaABuscar == null)
            {
                return NotFound();
            }



            string correoUsuario = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;

            Persona personaABuscar = new Persona
            {
                Correo = correoUsuario
            };

            personaABuscar = await personaService.ObtenerPersonaPorCorreo(personaABuscar);


            if (!pedidoService.VerificarPedidoPerteneceUsuario(facturaABuscar.IdPedidoNavigation, personaABuscar.IdPersona))
            {
                return Unauthorized();
            }

            return View();
        }


        public async Task<IActionResult> VerPedido([FromQuery] int idPedido)
        {
            Pedido pedidoABuscar = await pedidoService.ObtenerPedidoPorId(idPedido);


            string correoUsuario = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;

            Persona personaABuscar = new Persona
            {
                Correo = correoUsuario
            };

            personaABuscar = await personaService.ObtenerPersonaPorCorreo(personaABuscar);

            if (pedidoABuscar == null)
            {
                return NotFound();
            }


            if (!pedidoService.VerificarPedidoPerteneceUsuario(pedidoABuscar, personaABuscar.IdPersona))
            {
                return Unauthorized();
            }

            return View();
        }


        public async Task<IActionResult> ModificarSinpe([FromQuery] int idPedido)
        {
           



            Pedido pedidoABuscar = await pedidoService.ObtenerPedidoPorId(idPedido);


            string correoUsuario = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;

            Persona personaABuscar = new Persona
            {
                Correo = correoUsuario
            };

            personaABuscar = await personaService.ObtenerPersonaPorCorreo(personaABuscar);

            if (pedidoABuscar == null)
            {
                return NotFound();
            }

;
            if (!pedidoService.VerificarPedidoPerteneceUsuario(pedidoABuscar, personaABuscar.IdPersona))
            {
                return Unauthorized();
            }

            Pagossinpe pagoBuscado = await pagoSinpeService.ObtenerPagoSinpePorPedido(pedidoABuscar.IdPedido);

            if (pagoBuscado == null)
            {
                return NotFound();
            }



            return View();
        }

        public async Task<JsonResult> ObtenerTodosLosTiposDeEnvio()
        {
            return new JsonResult(new { arregloTiposDeEnvio = await tiposDeEnvioService.ObtenerTodosLosTiposDeEnvio() });

        }

        public async Task<JsonResult> ObtenerDireccionesUsuario()
        {
            string correoUsuario = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;

            Persona personaABuscar = new Persona
            {
                Correo = correoUsuario
            };

            personaABuscar = await personaService.ObtenerPersonaPorCorreo(personaABuscar);

            Direccionesusuario direccion = new Direccionesusuario
            {
                IdPersona = personaABuscar.IdPersona
            };


            return new JsonResult(new { arregloDirecciones = await direccionesService.ObtenerTodasLasDireccionesPorUsuario(direccion) });

        }

        public async Task<JsonResult> ObtenerTodosLosTiposDePago()
        {
            return new JsonResult(new { arregloTiposDePago = await tiposDePagoService.ObtenerTodosLosTiposDePago() });

        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> GuardarPedido([FromForm] PedidoViewModel pedido)
        {
            try
            {
                Pedido pedidoObjeto = PedidoViewModel.ConvertirPedidoViewModelAPedido(pedido);

                if (!pedidoService.VerificarDatosVaciosOIncorrectos(pedidoObjeto))
                {
                    return new JsonResult(new { mensaje = "Ha ocurrido un error al guardar el pedido", correcto = false });
                }



                string correoUsuario = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;

                Persona personaABuscar = new Persona
                {
                    Correo = correoUsuario
                };

                personaABuscar = await personaService.ObtenerPersonaPorCorreo(personaABuscar);


                Direccionesusuario direccion = new Direccionesusuario
                {
                    IdPersona = personaABuscar.IdPersona
                };

                IEnumerable<DireccionDTO> todasLasDireccionesUsuario = await direccionesService.ObtenerTodasLasDireccionesPorUsuario(direccion);

                // Si el usuario no tiene direcciones registradas y el tipo de envio es a domicilio
                if (todasLasDireccionesUsuario.Count() == 0 && pedidoObjeto.IdTipoEnvio == 1)
                {
                    return new JsonResult(new { mensaje = "No puede realizar un pedido con entrega a domicilio por que no tiene direcciones registradas", correcto = false });
                }

                // Se asigna el id de la persona logueada al id del pedido
                pedidoObjeto.IdPersona = personaABuscar.IdPersona;

                // Se asigna el id de estado pedido como 1 (Recibido)
                pedidoObjeto.IdEstadoPedido = 1;

                //Se establece la fecha del pedido como la fecha actual
                pedidoObjeto.FechaPedido = DateTime.Now;


                // Se buscan los elementos del carrito del usuario 

                IEnumerable<CarritoDTO> todosLosElementosDelCarrito = await carritoService.ObtenerCarritoUsuario(personaABuscar.IdPersona);




                // Tipo de pago efectivo
                if (pedidoObjeto.IdTipoPago == 1)
                {

                    bool correoEnviado = await correoService.EnviarCorreoPedidoConfirmado(personaABuscar, "Pedido confirmado", todosLosElementosDelCarrito, pedido);


                    if (!correoEnviado)
                    {
                        return new JsonResult(new { mensaje = "Ha ocurrido un error enviando el correo con los detalles del pedido", correcto = false });
                    }

                    await pedidoService.Guardar(pedidoObjeto);



                    // Se insertan los elementos del carrito en los productos del pedido
                    foreach (CarritoDTO elementoCarrito in todosLosElementosDelCarrito)
                    {
                        Pedidoproducto productoPedido = CarritoDTO.ConvertirCarritoAProductoPedido(elementoCarrito, pedidoObjeto.IdPedido);
                        await productoPedidoService.Guardar(productoPedido);
                    }

                    // Se eliminan los elementos del carrito 
                    foreach (CarritoDTO elementoCarrito in todosLosElementosDelCarrito)
                    {
                        Carritocompra carritoNormal = CarritoDTO.ConvertirCarritoDTOACarrito(elementoCarrito);
                        await carritoService.Eliminar(carritoNormal);
                    }

                    // Se guarda la factura
                    bool resultadoGuardarFactura = await facturaService.GuardarFactura(pedidoObjeto);

                    if (!resultadoGuardarFactura)
                    {
                        return new JsonResult(new { mensaje = "Ha ocurrido un error guardando la factura", correcto = true });
                    }


                    return new JsonResult(new { mensaje = Url.Action("Gracias", "Pedido"), correcto = true });
                }

                // Tipo de Pago Sinpe
                if (pedidoObjeto.IdTipoPago == 2)
                {
                    // Si no se envia una imagen
                    if (!pagoSinpeService.VerificarPagoSinpeVacio(pedido.PagoSinpe))
                    {
                        return new JsonResult(new { mensaje = "La imagen del sinpe no puede estar vacia", correcto = false });
                    }



                    bool correoEnviado = await correoService.EnviarCorreoPedidoConfirmado(personaABuscar, "Pedido confirmado", todosLosElementosDelCarrito, pedido);


                    if (!correoEnviado)
                    {
                        return new JsonResult(new { mensaje = "Ha ocurrido un error enviando el correo con los detalles del pedido", correcto = false });
                    }



                    Pagossinpe pagoSinpe = PagoSinpeViewModel.ConvertirPagoSinpeViewModelAPagoSinpe(pedido.PagoSinpe);

                    bool guardarImagenSinpe = await funcionesUtiles.GuardarImagenEnSistemaSinpe(pagoSinpe);
                    if (!guardarImagenSinpe)
                    {
                        return new JsonResult(new { mensaje = "Ha ocurrido un error guardando la imagen del sinpe", correcto = false });
                    }


                    await pedidoService.Guardar(pedidoObjeto);

                    // Se asigna el id del pago al pedido recien guardado

                    pagoSinpe.IdPedido = pedidoObjeto.IdPedido;

                    await pagoSinpeService.GuardarPagoSinpe(pagoSinpe);


                    // Se insertan los elementos del carrito en los productos del pedido
                    foreach (CarritoDTO elementoCarrito in todosLosElementosDelCarrito)
                    {
                        Pedidoproducto productoPedido = CarritoDTO.ConvertirCarritoAProductoPedido(elementoCarrito, pedidoObjeto.IdPedido);
                        await productoPedidoService.Guardar(productoPedido);
                    }

                    // Se eliminan los elementos del carrito 
                    foreach (CarritoDTO elementoCarrito in todosLosElementosDelCarrito)
                    {
                        Carritocompra carritoNormal = CarritoDTO.ConvertirCarritoDTOACarrito(elementoCarrito);
                        await carritoService.Eliminar(carritoNormal);
                    }


                    // Se guarda la factura
                    bool resultadoGuardarFactura = await facturaService.GuardarFactura(pedidoObjeto);

                    if (!resultadoGuardarFactura)
                    {
                        return new JsonResult(new { mensaje = "Ha ocurrido un error guardando la factura", correcto = true });
                    }


                    return new JsonResult(new { mensaje = Url.Action("Gracias", "Pedido"), correcto = true });
                }

                if (pedidoObjeto.IdTipoPago == 3)
                {

                    string idSesionStripe = await pedidoService.CrearSesionCheckoutStripe(pedido, todosLosElementosDelCarrito, pedidoObjeto);

                    if (string.IsNullOrEmpty(idSesionStripe))
                    {
                        return new JsonResult(new { mensaje = "Ha ocurrido un error al realizar el pago", correcto = false });
                    }




              



                    return new JsonResult(new { mensaje = "Pedido guardado con exito con metodo de pago en tarjeta", correcto = true, stripeId = idSesionStripe });

                }



                return new JsonResult(new { mensaje = "Ha ocurrido un error", correcto = false });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { mensaje = "Ha ocurrido un error al guardar el pedido", correcto = false });
            }
        }

      
        public async Task<IActionResult> ObtenerPedidosUsuarioLogueado()
        {
            string correoUsuario = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;

            Persona personaABuscar = new Persona
            {
                Correo = correoUsuario
            };

            personaABuscar = await personaService.ObtenerPersonaPorCorreo(personaABuscar);


            return new JsonResult(new { arregloPedidos = await pedidoService.ObtenerPedidoPorCliente(personaABuscar.IdPersona) });
        }



        [HttpGet("/Pedido/ObtenerPedidoDTOPorId/{idPedido}")]

        public async Task<IActionResult> ObtenerPedidoPorId(int idPedido)
        {
            PedidoDTO pedidoBuscado = await pedidoService.ObtenerPedidoPorIdDTO(idPedido);

            string correoUsuario = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;

            Persona personaABuscar = new Persona
            {
                Correo = correoUsuario
            };

            personaABuscar = await personaService.ObtenerPersonaPorCorreo(personaABuscar);

            Pedido pedido = new Pedido
            {
                IdPersona = pedidoBuscado.IdPersona,
            };

            if (!pedidoService.VerificarPedidoPerteneceUsuario(pedido, personaABuscar.IdPersona))
            {
                return Unauthorized();
            }


            return new JsonResult(new { pedido = await pedidoService.ObtenerPedidoPorIdDTO(idPedido) });
        }

        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CancelarPedido([FromBody] Pedido pedido)
        {
            try
            {
                if (pedido == null)
                {
                    return new JsonResult(new { mensaje = "Ha ocurrido un error al cancelar el pedido", correcto = false });
                }

                Pedido pedidoAModificar = await pedidoService.ObtenerPedidoPorId(pedido.IdPedido);

                if (pedidoAModificar == null)
                {
                    return new JsonResult(new { mensaje = "Ha ocurrido un error al cancelar el pedido", correcto = false });
                }



                string correoUsuario = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;

                Persona personaABuscar = new Persona
                {
                    Correo = correoUsuario
                };


                personaABuscar = await personaService.ObtenerPersonaPorCorreo(personaABuscar);


                if (pedidoAModificar.IdEstadoPedido == 5)
                {
                    return new JsonResult(new { mensaje = "El pedido ya se encuentra cancelado", correcto = false });
                }

                if (!pedidoService.VerificarPedidoPerteneceUsuario(pedidoAModificar, personaABuscar.IdPersona))
                {
                    return new JsonResult(new { mensaje = "Ha ocurrido un error", correcto = false });
                }



                // Se asigna el id 5 (cancelado)
                pedidoAModificar.IdEstadoPedido = 5;


                await pedidoService.Editar(pedidoAModificar);

                return new JsonResult(new { mensaje = "Pedido cancelado con exito", correcto = true });

            }
            catch (Exception ex)
            {
                return new JsonResult(new { mensaje = "Ha ocurrido un error al cancelar el pedido", correcto = false });
            }
        }


        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ModificarPagoSinpe([FromForm] PedidoViewModel pedido)
        {
            try
            {

                // Si no se envia una imagen
                if (!pagoSinpeService.VerificarPagoSinpeVacio(pedido.PagoSinpe))
                {
                    return new JsonResult(new { mensaje = "La imagen del sinpe no puede estar vacia", correcto = false });
                }


                Pedido pedidoObjeto = PedidoViewModel.ConvertirPedidoViewModelAPedido(pedido);

                Pedido pedidoAModificar = await pedidoService.ObtenerPedidoPorId(pedidoObjeto.IdPedido);

                if (pedidoAModificar == null)
                {
                    return new JsonResult(new { mensaje = "Ha ocurrido un error al modificar la imagen del sinpe", correcto = false });
                }



                string correoUsuario = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;

                Persona personaABuscar = new Persona
                {
                    Correo = correoUsuario
                };


                personaABuscar = await personaService.ObtenerPersonaPorCorreo(personaABuscar);


                if (pedidoAModificar.IdEstadoPedido == 5)
                {
                    return new JsonResult(new { mensaje = "No se puede modificar la imagen del sinpe ya que el pedido esta cancelado", correcto = false });
                }

                if (!pedidoService.VerificarPedidoPerteneceUsuario(pedidoAModificar, personaABuscar.IdPersona))
                {
                    return new JsonResult(new { mensaje = "Ha ocurrido un error", correcto = false });
                }

               

            

                Pagossinpe pagoSinpeAModificar = await pagoSinpeService.ObtenerPagoSinpePorPedido(pedidoAModificar.IdPedido);


                if (!funcionesUtiles.BorrarImagenGuardadaEnSistemaPagoSinpe(pagoSinpeAModificar))
                {
                    return new JsonResult(new { mensaje = "Ha ocurrido un error al eliminar la imagen del sinpe anterior", correcto = false });
                }

                pagoSinpeAModificar.ArchivoSinpe =  pedido.PagoSinpe.ArchivoSinpe;

                bool guardarImagenSinpe = await funcionesUtiles.GuardarImagenEnSistemaSinpe(pagoSinpeAModificar);

                if (!guardarImagenSinpe)
                {
                    return new JsonResult(new { mensaje = "Ha ocurrido un error guardando la imagen del sinpe", correcto = false });
                }

                await pagoSinpeService.EditarPagoSinpe(pagoSinpeAModificar);
                return new JsonResult(new { mensaje = Url.Action("PerfilUsuario", "UsuarioRegistrado"), correcto = true, mensajeInfo = "Imagen modificada con exito"});

            }
            catch (Exception ex)
            {
                return new JsonResult(new { mensaje = "Ha ocurrido un error al modificar la imagen del sinpe", correcto = false });
            }
        }


        [HttpGet("/Pedido/ObtenerFacturaPorIdPedido/{idPedido}")]
        public async Task<IActionResult> ObtenerFacturaPorIdPedido(int idPedido)
        {

            Factura facturaBuscada = await facturaService.ObtenerPorIdPedido(idPedido);

            string correoUsuario = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;

            Persona personaABuscar = new Persona
            {
                Correo = correoUsuario
            };

           
            personaABuscar = await personaService.ObtenerPersonaPorCorreo(personaABuscar);

           

            if (!pedidoService.VerificarPedidoPerteneceUsuario(facturaBuscada.IdPedidoNavigation, personaABuscar.IdPersona))
            {
                return Unauthorized();
            }


            return new JsonResult(new { factura = await facturaService.ObtenerPorIdFactura(facturaBuscada.IdFactura) });
        }

    }
}
