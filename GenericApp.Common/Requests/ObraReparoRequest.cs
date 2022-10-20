using System;
using System.ComponentModel.DataAnnotations;
namespace GenericApp.Common.Requests
{
    public class ObraReparoRequest
    {
        [Required]
        public int NROREGISTRO { get; set; }
        public DateTime? FECHACUMPLIMENTO { get; set; }
    }
}