using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace RowingApp.Web.Models
{
    public class AddProductImageViewModel
    {
        public int ProductId { get; set; }

        [Display(Name = "Imagen")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public IFormFile ImageFile { get; set; }
    }
}