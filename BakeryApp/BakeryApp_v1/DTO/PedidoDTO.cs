using BakeryApp_v1.Models;
using Microsoft.Extensions.Logging.Abstractions;
using static Mysqlx.Crud.Order.Types;

namespace BakeryApp_v1.DTO
{
    public class PedidoDTO
    {
        public int IdPedido { get; set; }

        public int IdEstadoPedido { get; set; }

        public int IdPersona { get; set; }

        public int IdTipoEnvio { get; set; }

        public int IdTipoPago { get; set; }

        public int? IdDireccion { get; set; }

        public string FechaPedido { get; set; }

        public DireccionDTO? Direccion { get; set; }

        public PagoSinpeDTO? PagoSinpe { get; set; }

        public EstadoPedidoDTO EstadoPedido { get; set; } = null!;

        public PersonaDTO Persona { get; set; } = null!;

        public TipoEnvioDTO TipoEnvio { get; set; } = null!;

        public TipoPagoDTO TipoPago { get; set; } = null!;

        public ICollection<ProductoPedidoDTO> ProductosPedido { get; set; } = new List<ProductoPedidoDTO>();


        public static PedidoDTO ConvertirPedidoAPedidoDTO(Pedido pedido)
        {
            if (pedido.IdTipoPago == 2 && pedido.IdTipoEnvio == 2)
            {
                return new PedidoDTO
                {
                    IdPedido = pedido.IdPedido,
                    IdEstadoPedido = pedido.IdEstadoPedido,
                    IdPersona = pedido.IdPersona,
                    IdTipoPago = pedido.IdTipoPago,
                    IdDireccion = pedido.IdDireccion,
                    FechaPedido = pedido.FechaPedido.ToString("yyyy/MM/dd HH:mm:ss"), // Ajuste del formato de fecha y hora

                    Persona = new PersonaDTO
                    {
                        Nombre = pedido.IdPersonaNavigation.Nombre,
                        Correo = pedido.IdPersonaNavigation.Correo
                    },
                    TipoEnvio = new TipoEnvioDTO
                    {
                        IdTipoEnvio = pedido.IdTipoEnvioNavigation.IdTipoEnvio,
                        NombreTipo = pedido.IdTipoEnvioNavigation.NombreTipo
                    },
                    TipoPago = new TipoPagoDTO
                    {
                        IdTipoPago = pedido.IdTipoPagoNavigation.IdTipoPago,
                        NombreTipo = pedido.IdTipoPagoNavigation.NombreTipo
                    },
                    EstadoPedido = new EstadoPedidoDTO
                    {
                        IdEstadoPedido = pedido.IdEstadoPedidoNavigation.IdEstadoPedido,
                        NombreEstado = pedido.IdEstadoPedidoNavigation.NombreEstado
                    },
                    PagoSinpe = new PagoSinpeDTO
                    {
                        IdPagoSinpe = pedido.Pagossinpes.FirstOrDefault().IdPagoSinpe,
                        RutaImagenSinpe = pedido.Pagossinpes.FirstOrDefault().RutaImagenSinpe,
                    },
                    ProductosPedido = pedido.Pedidoproductos.Select(productoPedido => new ProductoPedidoDTO
                    {
                        IdPedidoProducto = productoPedido.IdPedidoProducto,
                        IdProducto = productoPedido.IdProducto,
                        CantidadProducto = productoPedido.CantidadProducto,
                        Producto = new ProductoDTO
                        {
                            NombreProducto = productoPedido.IdProductoNavigation.NombreProducto,
                            PrecioProducto = productoPedido.IdProductoNavigation.PrecioProducto
                        }
                    }).ToList()
                };
            }


            if (pedido.IdTipoEnvio == 1 && pedido.IdTipoPago == 2)
            {
                return new PedidoDTO
                {
                    IdPedido = pedido.IdPedido,
                    IdEstadoPedido = pedido.IdEstadoPedido,
                    IdPersona = pedido.IdPersona,
                    IdTipoPago = pedido.IdTipoPago,
                    IdDireccion = pedido.IdDireccion,
                    FechaPedido = pedido.FechaPedido.ToString("yyyy/MM/dd HH:mm:ss"), // Ajuste del formato de fecha y hora
                    Direccion = new DireccionDTO
                    {
                        NombreDireccion = pedido.IdDireccionNavigation.NombreDireccion,
                        DireccionExacta = pedido.IdDireccionNavigation.DireccionExacta,
                        ProvinciaDTO = new ProvinciaDTO
                        {
                            IdProvincia = pedido.IdDireccionNavigation.IdProvinciaNavigation.IdProvincia,
                            NombreProvincia = pedido.IdDireccionNavigation.IdProvinciaNavigation.NombreProvincia,
                        },
                        CantonDTO = new CantonDTO
                        {
                            IdCanton = pedido.IdDireccionNavigation.IdCantonNavigation.IdCanton,
                            NombreCanton = pedido.IdDireccionNavigation.IdCantonNavigation.NombreCanton,
                        },
                        DistritoDTO = new DistritoDTO
                        {
                            IdDistrito = pedido.IdDireccionNavigation.IdDistritoNavigation.IdDistrito,
                            NombreDistrito = pedido.IdDireccionNavigation.IdDistritoNavigation.NombreDistrito
                        }
                    },
                    Persona = new PersonaDTO
                    {
                        Nombre = pedido.IdPersonaNavigation.Nombre,
                        Correo = pedido.IdPersonaNavigation.Correo
                    },
                    TipoEnvio = new TipoEnvioDTO
                    {
                        IdTipoEnvio = pedido.IdTipoEnvioNavigation.IdTipoEnvio,
                        NombreTipo = pedido.IdTipoEnvioNavigation.NombreTipo
                    },
                    TipoPago = new TipoPagoDTO
                    {
                        IdTipoPago = pedido.IdTipoPagoNavigation.IdTipoPago,
                        NombreTipo = pedido.IdTipoPagoNavigation.NombreTipo
                    },
                    EstadoPedido = new EstadoPedidoDTO
                    {
                        IdEstadoPedido = pedido.IdEstadoPedidoNavigation.IdEstadoPedido,
                        NombreEstado = pedido.IdEstadoPedidoNavigation.NombreEstado
                    },
                    PagoSinpe = new PagoSinpeDTO
                    {
                        IdPagoSinpe = pedido.Pagossinpes.FirstOrDefault().IdPagoSinpe,
                        RutaImagenSinpe = pedido.Pagossinpes.FirstOrDefault().RutaImagenSinpe,
                    },
                    ProductosPedido = pedido.Pedidoproductos.Select(productoPedido => new ProductoPedidoDTO
                    {
                        IdPedidoProducto = productoPedido.IdPedidoProducto,
                        IdProducto = productoPedido.IdProducto,
                        CantidadProducto = productoPedido.CantidadProducto,
                        Producto = new ProductoDTO
                        {
                            NombreProducto = productoPedido.IdProductoNavigation.NombreProducto,
                            PrecioProducto = productoPedido.IdProductoNavigation.PrecioProducto
                        }
                    }).ToList()
                };
            }
            else if (pedido.IdTipoEnvio == 1)
            {
                return new PedidoDTO
                {
                    IdPedido = pedido.IdPedido,
                    IdEstadoPedido = pedido.IdEstadoPedido,
                    IdPersona = pedido.IdPersona,
                    IdTipoPago = pedido.IdTipoPago,
                    IdDireccion = pedido.IdDireccion,
                    FechaPedido = pedido.FechaPedido.ToString("yyyy/MM/dd HH:mm:ss"), // Ajuste del formato de fecha y hora
                    Direccion = new DireccionDTO
                    {
                        NombreDireccion = pedido.IdDireccionNavigation.NombreDireccion,
                        DireccionExacta = pedido.IdDireccionNavigation.DireccionExacta,
                        ProvinciaDTO = new ProvinciaDTO
                        {
                            IdProvincia = pedido.IdDireccionNavigation.IdProvinciaNavigation.IdProvincia,
                            NombreProvincia = pedido.IdDireccionNavigation.IdProvinciaNavigation.NombreProvincia,
                        },
                        CantonDTO = new CantonDTO
                        {
                            IdCanton = pedido.IdDireccionNavigation.IdCantonNavigation.IdCanton,
                            NombreCanton = pedido.IdDireccionNavigation.IdCantonNavigation.NombreCanton,
                        },
                        DistritoDTO = new DistritoDTO
                        {
                            IdDistrito = pedido.IdDireccionNavigation.IdDistritoNavigation.IdDistrito,
                            NombreDistrito = pedido.IdDireccionNavigation.IdDistritoNavigation.NombreDistrito
                        }
                    },
                    Persona = new PersonaDTO
                    {
                        Nombre = pedido.IdPersonaNavigation.Nombre,
                        Correo = pedido.IdPersonaNavigation.Correo
                    },
                    TipoEnvio = new TipoEnvioDTO
                    {
                        IdTipoEnvio = pedido.IdTipoEnvioNavigation.IdTipoEnvio,
                        NombreTipo = pedido.IdTipoEnvioNavigation.NombreTipo
                    },
                    TipoPago = new TipoPagoDTO
                    {
                        IdTipoPago = pedido.IdTipoPagoNavigation.IdTipoPago,
                        NombreTipo = pedido.IdTipoPagoNavigation.NombreTipo
                    },
                    EstadoPedido = new EstadoPedidoDTO
                    {
                        IdEstadoPedido = pedido.IdEstadoPedidoNavigation.IdEstadoPedido,
                        NombreEstado = pedido.IdEstadoPedidoNavigation.NombreEstado
                    },
                    ProductosPedido = pedido.Pedidoproductos.Select(productoPedido => new ProductoPedidoDTO
                    {
                        IdPedidoProducto = productoPedido.IdPedidoProducto,
                        IdProducto = productoPedido.IdProducto,
                        CantidadProducto = productoPedido.CantidadProducto,
                        Producto = new ProductoDTO
                        {
                            NombreProducto = productoPedido.IdProductoNavigation.NombreProducto,
                            PrecioProducto = productoPedido.IdProductoNavigation.PrecioProducto
                        }
                    }).ToList()
                };
            } else if (pedido.IdTipoEnvio == 2)
            {
                return new PedidoDTO
                {
                    IdPedido = pedido.IdPedido,
                    IdEstadoPedido = pedido.IdEstadoPedido,
                    IdPersona = pedido.IdPersona,
                    IdTipoPago = pedido.IdTipoPago,
                    IdDireccion = pedido.IdDireccion,
                    FechaPedido = pedido.FechaPedido.ToString("yyyy/MM/dd HH:mm:ss"), // Ajuste del formato de fecha y hora
                   
                    Persona = new PersonaDTO
                    {
                        Nombre = pedido.IdPersonaNavigation.Nombre,
                        Correo = pedido.IdPersonaNavigation.Correo
                    },
                    TipoEnvio = new TipoEnvioDTO
                    {
                        IdTipoEnvio = pedido.IdTipoEnvioNavigation.IdTipoEnvio,
                        NombreTipo = pedido.IdTipoEnvioNavigation.NombreTipo
                    },
                    TipoPago = new TipoPagoDTO
                    {
                        IdTipoPago = pedido.IdTipoPagoNavigation.IdTipoPago,
                        NombreTipo = pedido.IdTipoPagoNavigation.NombreTipo
                    },
                    EstadoPedido = new EstadoPedidoDTO
                    {
                        IdEstadoPedido = pedido.IdEstadoPedidoNavigation.IdEstadoPedido,
                        NombreEstado = pedido.IdEstadoPedidoNavigation.NombreEstado
                    },
                    ProductosPedido = pedido.Pedidoproductos.Select(productoPedido => new ProductoPedidoDTO
                    {
                        IdPedidoProducto = productoPedido.IdPedidoProducto,
                        IdProducto = productoPedido.IdProducto,
                        CantidadProducto = productoPedido.CantidadProducto,
                        Producto = new ProductoDTO
                        {
                            NombreProducto = productoPedido.IdProductoNavigation.NombreProducto,
                            PrecioProducto = productoPedido.IdProductoNavigation.PrecioProducto
                        }
                    }).ToList()
                };
            } else
            {
                return new PedidoDTO();
            }


        }




    }
}
