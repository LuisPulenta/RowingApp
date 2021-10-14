using System.ComponentModel.DataAnnotations;

namespace GenericApp.Common.Requests
{
    public class CausanteRequest
    {
        [Required]
        public string Codigo { get; set; }
    }
}