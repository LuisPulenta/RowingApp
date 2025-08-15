using System;
using System.ComponentModel.DataAnnotations;
namespace RowingApp.Common.Requests
{
    public class ObraReparoRequest
    {
        [Required]
        public int NROREGISTRO { get; set; }
        public byte[] FotoInicioArray { get; set; }
        public byte[] FotoFinArray { get; set; }
        public string ObservacionesFotoInicio { get; set; }
        public string ObservacionesFotoFin { get; set; }
        public int? Largo2 { get; set; }
        public int? Ancho2 { get; set; }
    }
}