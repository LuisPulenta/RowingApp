using System.ComponentModel.DataAnnotations;

namespace RowingApp.Common.Requests
{
    public class ObrasNuevoSuministrosDe
    {
        [Required]
        public string ASTICKET { get; set; }
    }
}