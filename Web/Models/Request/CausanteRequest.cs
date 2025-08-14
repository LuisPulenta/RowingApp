using System.ComponentModel.DataAnnotations;

namespace RowingApp.Common.Requests
{
    public class CausanteRequest
    {
        [Required]
        public string Codigo { get; set; }
    }
}