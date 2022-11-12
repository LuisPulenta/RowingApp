using System;
using System.ComponentModel.DataAnnotations;
namespace GenericApp.Common.Requests
{
    public class ObraReparoRequest
    {
        [Required]
        public int NROREGISTRO { get; set; }
        public DateTime? FECHACUMPLIMENTO { get; set; }
        public byte[] FotoInicioArray { get; set; }
        public byte[] FotoFinArray { get; set; }
        public string ObservacionesFotoInicio { get; set; }
        public string ObservacionesFotoFin { get; set; }
    }
}