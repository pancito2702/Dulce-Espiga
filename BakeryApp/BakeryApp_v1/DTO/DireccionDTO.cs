using BakeryApp_v1.Models;
using static Mysqlx.Crud.Order.Types;

namespace BakeryApp_v1.DTO
{
    public class DireccionDTO
    {
        public int IdDireccion { get; set; }
        public string NombreDireccion { get; set; } = null!;

        public string DireccionExacta { get; set; } = null!;

        public ProvinciaDTO ProvinciaDTO { get; set; } = null!;

        public CantonDTO CantonDTO { get; set; } = null!;

        public DistritoDTO DistritoDTO { get; set; } = null!;

        public static DireccionDTO ConvertirDireccionADireccionDTO(Direccionesusuario direccion)
        {
            return new DireccionDTO
            {
                IdDireccion = direccion.IdDireccion,
                DireccionExacta = direccion.DireccionExacta,
                NombreDireccion = direccion.NombreDireccion,

                ProvinciaDTO = new ProvinciaDTO
                {
                    IdProvincia = direccion.IdProvinciaNavigation.IdProvincia,
                    NombreProvincia = direccion.IdProvinciaNavigation.NombreProvincia
                },
                CantonDTO = new CantonDTO
                {
                    IdCanton = direccion.IdCantonNavigation.IdCanton,
                    NombreCanton = direccion.IdCantonNavigation.NombreCanton
                },
                DistritoDTO = new DistritoDTO
                {
                    IdDistrito = direccion.IdDistritoNavigation.IdDistrito,
                    NombreDistrito = direccion.IdDistritoNavigation.NombreDistrito
                }
            };
        }
      
    }
}
