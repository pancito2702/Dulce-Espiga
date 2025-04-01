using System.ComponentModel.DataAnnotations.Schema;

namespace BakeryApp_v1.Models
{
    public partial class Categoria
    {
        [NotMapped]
        public IFormFile ArchivoCategoria { get; set; }
    }
}
