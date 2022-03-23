using System.ComponentModel.DataAnnotations;
namespace GenericApp.Common.Requests
{
    public class VehiculoRequest
    {
        [Required]
        public string NUMCHA { get; set; }
    }
}
