using System.ComponentModel.DataAnnotations.Schema;

namespace BakeryApp_v1.Models
{
    public partial class Producto
    {

        [NotMapped]
        public IFormFile ArchivoProducto { get; set; }
    }
}
