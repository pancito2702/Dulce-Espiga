
using BakeryApp_v1.DTO;
using BakeryApp_v1.Models;
using BakeryApp_v1.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BakeryApp_v1.Controllers
{
    [Authorize(Policy = "SoloClientes")]
    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
    public class UsuarioRegistradoController : Controller
    {
        private readonly PersonaService personaService;
        private readonly ProvinciaService provinciaService;
        private readonly CantonService cantonService;
        private readonly DistritoService distritoService;
        private readonly DireccionesService direccionesService; 
        private readonly CategoriaService categoriaService;
        private readonly ProductoService productoService;
        private readonly BoletinService boletinService;
        private readonly ProductoPedidoService productoPedidoService;

        public UsuarioRegistradoController(PersonaService personaService, ProvinciaService provinciaService, CantonService cantonService, DistritoService distritoService, DireccionesService direccionesService, CategoriaService categoriaService, ProductoService productoService, BoletinService boletinService, ProductoPedidoService productoPedidoService)
        {
            this.personaService = personaService;
            this.provinciaService = provinciaService;
            this.cantonService = cantonService;
            this.distritoService = distritoService;
            this.direccionesService = direccionesService;
            this.categoriaService = categoriaService;   
            this.productoService = productoService;
            this.boletinService = boletinService;
            this.productoPedidoService = productoPedidoService;
        }


        [HttpGet("/UsuarioRegistrado/BuscarProducto/{nombreProducto}")]
        public async Task<IActionResult> BuscarProducto(string nombreProducto)
        {
            if (productoService.VerificarNombreVacio(nombreProducto))
            {
                return new JsonResult(new { mensaje = "El nombre del producto no puede estar vacio", correcto = false });
            }

            Producto productoBuscado = await productoService.BuscarProductoPorNombre(nombreProducto);

            if (productoBuscado == null)
            {
                return new JsonResult(new { mensaje = "El producto no se ha encontrado", correcto = false });
            }



            return new JsonResult(new { mensaje =  Url.Action("ProductoBuscado", "UsuarioRegistrado", new {producto = productoBuscado.NombreProducto}), correcto = true });
        }


    

        public async Task<IActionResult> ProductoBuscado([FromQuery]string producto)
        {
            Producto productoBuscado = await productoService.BuscarProductoPorNombre(producto);



            if (productoBuscado == null)
            {
                return NotFound();
            }


            return View();
        }


        [HttpGet]

        public async Task<IActionResult> ObtenerProductosMasVendidos()
        {
            return new JsonResult(new { arregloProductosMasVendidos = await productoPedidoService.ObtenerProductosMasVendidos() });
        }


        [HttpGet("/UsuarioRegistrado/VerDetallesProductoBuscado/{producto}")]
        public async Task<IActionResult> VerDetallesProductoBuscado(string producto)
        {

            if (productoService.VerificarNombreVacio(producto))
            {
                return new JsonResult(new { mensaje = "El nombre del producto no puede estar vacio", correcto = false });
            }

            Producto productoBuscado = await productoService.BuscarProductoPorNombre(producto);

            if (productoBuscado == null)
            {
                return new JsonResult(new { mensaje = "El producto no se ha encontrado", correcto = false });
            }
           


            return new JsonResult(new { mensaje = productoBuscado, correcto = true });
        }


        public IActionResult Index()
        {
            return View();
        }




        public async Task<IActionResult> TiendaUsuario([FromQuery] int pagina)
        {
            int totalPaginas = await categoriaService.CalcularTotalPaginas();
            if (pagina > totalPaginas)
            {
                return NotFound();
            }


            return View();
        }

       
        public async Task<IActionResult> ProductosPorCategoria([FromQuery]int categoria)
        {
            Categoria categoriaBuscada = await categoriaService.ObtenerCategoriaPorId(categoria);

            if (categoriaBuscada == null)
            {
                return NotFound();
            }


            return View();
        }



        public async Task<IActionResult> Producto([FromQuery] int producto)
        {
            Producto productoBuscado = await productoService.ObtenerProductoPorId(producto);

            if (productoBuscado == null)
            {
                return NotFound();
            }


            return View();
        }
      

        public IActionResult ServiciosUsuario()
        {
            return View();
        }

        public IActionResult SobreNosotrosUsuario()
        {
            return View();
        }

        public IActionResult ContactoUsuario()
        {
            return View();
        }

        public IActionResult PerfilUsuario()
        {

            
        
            return View();
        }


        [HttpGet("/UsuarioRegistrado/ObtenerProductosPorCategoria/{idCategoria}")]

        public async Task<IActionResult> ObtenerProductosPorCategoria(int idCategoria)
        {
            return new JsonResult(new { arregloProductos = await productoService.ObtenerTodasLasProductosPorCategoria(idCategoria) });
        }


        [HttpGet("/UsuarioRegistrado/ObtenerProductoPorId/{idProducto}")]

        public async Task<IActionResult> ObtenerProductoPorId(int idProducto)
        {
            return new JsonResult(new { producto = await productoService.ObtenerProductoPorId(idProducto) });
        }


        [HttpGet("/UsuarioRegistrado/ObtenerCategoriaPorId/{idCategoria}")]

        public async Task<IActionResult> ObtenerCategoriaPorId(int idCategoria)
        {
            return new JsonResult(new { categoria = await categoriaService.ObtenerCategoriaPorId(idCategoria) });
        }

        [HttpGet]

        public async Task<IActionResult> ObtenerTotalPaginas(int pagina)
        {
          

            return new JsonResult(new { paginas = await categoriaService.CalcularTotalPaginas() });
        }




        [HttpGet("/UsuarioRegistrado/ObtenerCategorias/{pagina}")]

        public async Task<IActionResult> ObtenerCategorias(int pagina)
        {
            if (pagina <= 0)
            {
                return NotFound();
            }

            return new JsonResult(new { arregloCategorias = await categoriaService.ObtenerTodasLasCategorias(pagina) });
        }



        [HttpGet]

        public async Task<IActionResult> ObtenerDireccionUsuario()
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




        [HttpDelete("/UsuarioRegistrado/EliminarDireccion/{idDireccion}")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> EliminarDireccion(int idDireccion)
        {
            try
            {
                if (!direccionesService.VerificarIdValidoDireccion(idDireccion))
                {
                    return new JsonResult(new { mensaje = "Identificador de la direccion invalido", correcto = false });
                }


                string correoUsuario = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;

                Persona personaABuscar = new Persona
                {
                    Correo = correoUsuario
                };

                personaABuscar = await personaService.ObtenerPersonaPorCorreo(personaABuscar);

                Direccionesusuario direccionAEliminar = await direccionesService.ObtenerDireccionPorId(idDireccion);


                if (!direccionesService.VerificarDireccionPerteneceUsuario(direccionAEliminar, personaABuscar))
                {
                    return new JsonResult(new { mensaje = "Esta direccion no le pertenece, por lo que no la puede eliminar", correcto = false });
                }

              
                await direccionesService.Eliminar(direccionAEliminar);

                return new JsonResult(new { mensaje = "Direccion eliminada con exito", correcto = true });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { mensaje = "Ha ocurrido un error", correcto = false });
            }
        }



        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> AgregarDireccion([FromBody] Direccionesusuario direccion)
        {
            try
            {
                if (direccionesService.DatosVaciosONulos(direccion))
                {
                    return new JsonResult(new { mensaje = "Hay datos vacios, por favor revise", correcto = false });
                }

                string correoUsuario = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;

                Persona personaABuscar = new Persona
                {
                    Correo = correoUsuario
                };

                personaABuscar = await personaService.ObtenerPersonaPorCorreo(personaABuscar);

                // Se le asignan las propiedades del usuario logueado a la direccion

                direccion.IdPersona = personaABuscar.IdPersona;


                bool nombreRepetido = await direccionesService.ObtenerDireccionPorNombre(direccion);

                if (nombreRepetido)
                {
                    return new JsonResult(new { mensaje = "El nombre de la direccion ya se encuentra registrado", correcto = false });
                }

             
                await direccionesService.Guardar(direccion);
                return new JsonResult(new { mensaje = "Direccion agregada correctamente", correcto = true });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { mensaje = "Ha ocurrido un error", correcto = false });
            }

        }



        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ModificarDireccion([FromBody] Direccionesusuario direccion)
        {
            try
            {
                if (direccion == null)
                {
                    return new JsonResult(new { mensaje = "No puedo modificar una direccion, ya que no hay direcciones registradas", correcto = false });
                }

                if (direccionesService.DatosVaciosONulos(direccion))
                {
                    return new JsonResult(new { mensaje = "Hay datos vacios, por favor revise", correcto = false });
                }

                string correoUsuario = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;

                Persona personaABuscar = new Persona
                {
                    Correo = correoUsuario
                };

                personaABuscar = await personaService.ObtenerPersonaPorCorreo(personaABuscar);

                // Se le asignan las propiedades del usuario logueado a la direccion

                direccion.IdPersona = personaABuscar.IdPersona;


                bool nombreRepetido = await direccionesService.ObtenerDireccionPorNombre(direccion);

                if (nombreRepetido)
                {
                    return new JsonResult(new { mensaje = "El nombre de la direccion ya se encuentra registrado", correcto = false });
                }

          
                await direccionesService.Editar(direccion);
                return new JsonResult(new { mensaje = "Direccion modificada correctamente", correcto = true });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { mensaje = "Ha ocurrido un error", correcto = false });
            }

        }

        [HttpGet]

        public async Task<IActionResult> ObtenerTodasProvincias()
        {
            return new JsonResult(new { arregloProvincias = await provinciaService.ObtenerTodasLasProvincias() });
        }

        [HttpGet("/UsuarioRegistrado/ObtenerTodasLosCantonesPorProvincia/{idProvincia}")]

        public async Task<IActionResult> ObtenerTodasLosCantonesPorProvincia(int idProvincia)
        {
            Provincia provincia = new Provincia
            {
                IdProvincia = idProvincia,
            };
            return new JsonResult(new { arregloCantones = await cantonService.ObtenerTodasLosCantonesPorProvincia(provincia)});
        }


        [HttpGet("/UsuarioRegistrado/ObtenerTodasLosDistritosPorCanton/{idCanton}")]

        public async Task<IActionResult> ObtenerTodasLosDistritosPorCanton(int idCanton)
        {
            Cantone canton = new Cantone
            {
                IdCanton = idCanton,
            };
            return new JsonResult(new { arregloDistritos = await distritoService.ObtenerTodasLosDistritosPorCanton(canton) });
        }



        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> EliminarUsuario([FromBody] Persona persona)
        {
            if (personaService.VerificarContraVacia(persona))
            {
                return new JsonResult(new { mensaje = "La contraseña no puede estar vacia", correcto = false });
            }

            string correoUsuario = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;

            Persona personaABuscar = new Persona
            {
                Correo = correoUsuario
            };

            Persona personaAEliminar = await personaService.ObtenerPersonaPorCorreo(personaABuscar);


            if (!personaService.VerificarContraConContraUsuario(persona, personaAEliminar))
            {
                return new JsonResult(new { mensaje = "Contraseña incorrecta", correcto = false });
            }

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await personaService.Eliminar(personaAEliminar);
            return new JsonResult(new { mensaje = Url.Action("Index", "Home"), correcto = true });
        }


        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarUsuario([FromBody] Persona persona)
        {
            try
            {
                if (personaService.VerificarNombreApellidosOTelefonoNulos(persona))
                {
                    return new JsonResult(new { mensaje = "Hay datos vacios, por favor revise", correcto = false });
                }


               


                if (!personaService.ValidarLongitudTelefono(persona))
                {
                    return new JsonResult(new { mensaje = "La longitud del telefono debe ser igual a 8 caracteres, con un guion", correcto = false });
                }

                if (!personaService.ValidarNumeroTelefono(persona))
                {
                    return new JsonResult(new { mensaje = "El numero de telefono no es valido", correcto = false });
                }

                string correoUsuario = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;

                Persona personaABuscar = new Persona
                {
                    Correo = correoUsuario
                };

                Persona personaAModificar = await personaService.ObtenerPersonaPorCorreo(personaABuscar);

            
                personaAModificar.Nombre = persona.Nombre;
                personaAModificar.PrimerApellido = persona.PrimerApellido;
                personaAModificar.SegundoApellido = persona.SegundoApellido;
                personaAModificar.Telefono = persona.Telefono;


                bool resultadoRepetidaTelefono = await personaService.VerificarTelefonoRepetido(personaAModificar);

                if (resultadoRepetidaTelefono)
                {
                    return new JsonResult(new { mensaje = "El telefono de la persona ya se encuentra registrado", correcto = false });
                }


                await personaService.Editar(personaAModificar);

                return new JsonResult(new { mensaje = "Perfil modificado con exito", correcto = true });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { mensaje = "Ha ocurrido un error inesperado", correcto = false });
            }
        }



        [HttpGet]

        public async Task<IActionResult> ObtenerDatosUsuarioLogueado()
        {
            try
            {
                string correoUsuario = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;

                Persona personaABuscar = new Persona
                {
                    Correo = correoUsuario
                };

                Persona personaLogueada = await personaService.ObtenerPersonaPorCorreo(personaABuscar);

                return new JsonResult(new { mensaje = PersonaDTO.ConvertirPersonaAPersonaDTOSinRoles(personaLogueada) });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { mensaje = "Ha ocurrido un error al obtener el usuario actual"});
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SuscribirseBoletin()
        {
            try
            {
                string correoUsuario = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;

                Persona personaABuscar = new Persona
                {
                    Correo = correoUsuario
                };

                Persona personaLogueada = await personaService.ObtenerPersonaPorCorreo(personaABuscar);


                Boletin boletinUsuario = await boletinService.VerificarUsuarioEnBoletin(personaLogueada.IdPersona);

                if (boletinUsuario is not null)
                {
                    return new JsonResult(new { mensaje = "El usuario ya se encuentra registrado en el boletin", correcto = false });
                }


                // Se estable el idBoletinNoticias como 1, ya que solo hay un boletin
                Boletin boletinAGuardar = new Boletin
                {
                    IdBoletinNoticias = 1,
                    IdUsuario = personaLogueada.IdPersona
                };

                await boletinService.Guardar(boletinAGuardar);

                return new JsonResult(new { mensaje = "Usuario registrado con exito en el boletin de noticias", correcto = true });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { mensaje = "Ha ocurrido un error al registrarse en el boletin", correcto = false });
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BorrarUsuarioBoletin()
        {
            try
            {
                string correoUsuario = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;

                Persona personaABuscar = new Persona
                {
                    Correo = correoUsuario
                };

                Persona personaLogueada = await personaService.ObtenerPersonaPorCorreo(personaABuscar);


                Boletin boletinUsuario = await boletinService.VerificarUsuarioEnBoletin(personaLogueada.IdPersona);

                if (boletinUsuario == null)
                {
                    return new JsonResult(new { mensaje = "El usuario no se encuentra registrado en el boletin por lo que no se puede borrar", correcto = false });
                }


                
                await boletinService.Eliminar(boletinUsuario);

                return new JsonResult(new { mensaje = "Usuario eliminado con exito del boletin de noticias", correcto = true });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { mensaje = "Ha ocurrido un error al eliminar el usuario del boletin", correcto = false });
            }
        }


    }
}
