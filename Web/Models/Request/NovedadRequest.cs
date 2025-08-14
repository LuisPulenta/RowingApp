using System.ComponentModel.DataAnnotations;
namespace RowingApp.Common.Requests
{
    public class NovedadRequest
    {
        [Required]
        public int IDNOVEDAD { get; set; }
    }
}