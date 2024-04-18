using System;
using System.ComponentModel.DataAnnotations;

namespace GenericApp.Common.Requests
{
    public class DateRequest
    {
        [Required]
        public DateTime Fecha { get; set; }
    }
}