using System;
using System.ComponentModel.DataAnnotations;

namespace GenericApp.Common.Requests
{
    public class ObraFechaCierreElectricoRequest
    {
        [Required]
        public int NroObra { get; set; }
        public DateTime FechaCierreElectrico { get; set; }
    }
}