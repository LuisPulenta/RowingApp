using System.ComponentModel.DataAnnotations;

namespace GenericApp.Common.Requests
{
    public class ReciboRequest
    {
        [Required]
        public int IdRecibo { get; set; }
        public string Latitud { get; set; }
        public string Longitud { get; set; }
        public string FileName { get; set; }
        public byte[] ImageArray { get; set; }
        public string Imei { get; set; }
    }
}