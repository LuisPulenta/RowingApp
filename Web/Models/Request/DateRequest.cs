using System;
using System.ComponentModel.DataAnnotations;

namespace RowingApp.Common.Requests
{
    public class DateRequest
    {
        [Required]
        public DateTime Fecha { get; set; }
    }
}