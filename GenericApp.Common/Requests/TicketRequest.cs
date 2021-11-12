using System.ComponentModel.DataAnnotations;

namespace GenericApp.Common.Requests
{
    public class TicketRequest
    {
        [Required]
        public string ASTICKET { get; set; }
    }
}