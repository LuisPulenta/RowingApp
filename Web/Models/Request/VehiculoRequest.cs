using System.ComponentModel.DataAnnotations;
namespace RowingApp.Common.Requests
{
    public class VehiculoRequest
    {
        [Required]
        public string NUMCHA { get; set; }
    }
}
