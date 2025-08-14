using System;
using System.ComponentModel.DataAnnotations;
namespace RowingApp.Common.Requests
{
    public class Vehiculo3Request
    {
        [Required]
        public int NroInterno { get; set; }

        public int? KMHSACTUAL { get; set; }
    }
}