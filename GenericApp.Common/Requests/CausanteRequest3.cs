using System.ComponentModel.DataAnnotations;

namespace GenericApp.Common.Requests
{
    public class CausanteRequest3
    {
        [Required]
        public string Grupo { get; set; }
        [Required]
        public string Codigo { get; set; }
    }
}