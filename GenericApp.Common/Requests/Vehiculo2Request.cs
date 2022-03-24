using System;
using System.ComponentModel.DataAnnotations;
namespace GenericApp.Common.Requests
{
    public class Vehiculo2Request
    {
        [Required]
        public Int16 Id { get; set; }

        public int? KMHSACTUAL { get; set; }
    }
}
