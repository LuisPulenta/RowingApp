using System.ComponentModel.DataAnnotations;

namespace GenericApp.Common.Requests
{
    public class MaximoRequest
    {
        [Required]
        public string Grupo { get; set; }
        [Required]
        public string Causante { get; set; }
        [Required]
        public string Catalogo { get; set; }
        [Required]
        public decimal Cantidad { get; set; }
    }
}