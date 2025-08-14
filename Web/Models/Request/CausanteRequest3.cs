using System.ComponentModel.DataAnnotations;

namespace RowingApp.Common.Requests
{
    public class CausanteRequest3
    {
        [Required]
        public string Grupo { get; set; }
        [Required]
        public string Codigo { get; set; }
    }
}