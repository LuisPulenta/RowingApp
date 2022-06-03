using System.ComponentModel.DataAnnotations;
namespace GenericApp.Common.Requests
{
    public class NovedadRequest
    {
        [Required]
        public int IDNOVEDAD { get; set; }
    }
}