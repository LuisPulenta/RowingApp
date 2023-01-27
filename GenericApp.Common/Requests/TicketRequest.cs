using System.ComponentModel.DataAnnotations;

namespace GenericApp.Common.Requests
{
    public class ObrasNuevoSuministrosDe
    {
        [Required]
        public string ASTICKET { get; set; }
    }
}