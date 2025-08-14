using Common.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace RowingApp.Web.Data.Entities
{
    public class User : IdentityUser
    {
        public int NroCausante { get; set; }

        public DateTime? LastLogin { get; set; }
        public DateTime? ChangePassword { get; set; }

        [MaxLength(20, ErrorMessage = "El campo {0} debe contener menos de {1} caracteres")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Documento")]
        public string Document { get; set; }

        [Display(Name = "Nombre")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe contener menos de {1} caracteres")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string FirstName { get; set; }

        [Display(Name = "Apellido")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe contener menos de {1} caracteres")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string LastName { get; set; }

        [Display(Name = "Tipo de Usuario")]
        public UserType UserType { get; set; }

        [MaxLength(6, ErrorMessage = "El campo {0} debe contener menos de {1} caracteres")]
        public string codigo { get; set; }
        [MaxLength(3, ErrorMessage = "El campo {0} debe contener menos de {1} caracteres")]
        public string grupo { get; set; }

        [Display(Name = "Usuario")]
        public string FullName => $"{FirstName} {LastName}";

        [Display(Name = "Usuario")]
        public string FullNameWithDocument => $"{FirstName} {LastName} - {Document}";
    }

}