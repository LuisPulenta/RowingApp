using RowingApp.Web.Data.Entities;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace RowingApp.Web.Models
{
    public class CountryViewModel : CountryEntity
    {
        [Display(Name = "Imagen")]
        public IFormFile ImageFile { get; set; }
    }
}