using System.ComponentModel.DataAnnotations.Schema;

namespace BakeryApp_v1.Models
{
    public partial class Pagossinpe
    {
        [NotMapped]
        public IFormFile ArchivoSinpe { get; set; }
    }
}
