using System.ComponentModel.DataAnnotations;
namespace RowingApp.Common.Requests
{
    public class Vehiculo2Request
    {
        [Required]
        public int Id { get; set; }

        public int? KMHSACTUAL { get; set; }
    }
}
