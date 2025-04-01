using BakeryApp_v1.Models;

namespace BakeryApp_v1.DTO
{
    public class BoletinDTO
    {
        public int IdBoletin { get; set; }

        public int IdBoletinNoticias { get; set; }

        public int IdUsuario { get; set; }

        public PersonaDTO Persona { get; set; } = null!;

    }
}
