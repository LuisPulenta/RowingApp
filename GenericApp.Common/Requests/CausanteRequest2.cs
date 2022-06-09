using System.ComponentModel.DataAnnotations;

namespace GenericApp.Common.Requests
{
    public class CausanteRequest2
    {
        [Required]
        public int Id { get; set; }

        public string telefono { get; set; }
    }
}