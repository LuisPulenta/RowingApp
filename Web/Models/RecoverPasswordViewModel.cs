using System.ComponentModel.DataAnnotations;

namespace RowingApp.Web.Models
{
    public class RecoverPasswordViewModel
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [EmailAddress]
        public string Email { get; set; }
    }
}