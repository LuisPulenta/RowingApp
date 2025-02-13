using System.ComponentModel.DataAnnotations;

namespace GenericApp.Common.Requests
{
    public class ReciboRequest
    {
        [Required]
        public int IdRecibo { get; set; }
        public string Latitud { get; set; }
        public string Longitud { get; set; }
    }
}