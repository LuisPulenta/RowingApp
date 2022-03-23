using System;

namespace GenericApp.Common.Responses
{
    public class VehiculosKilometrajeResponse
    {
        public int Orden { get; set; }
        public string Equipo { get; set; }
        public DateTime Fecha { get; set; }
        public int? KILINI { get; set; }
        public int? KILFIN { get; set; }
        public DateTime FECHAALTA { get; set; }
        public DateTime KMFECHAANTERIOR { get; set; }
        public int? HORSAL { get; set; }
        public int? HORLLE { get; set; }
        public int? CODSUC { get; set; }
        public int? NRODEOT { get; set; }
        public int? CAMBIO { get; set; }
        public int? PROCESADO { get; set; }
        public int? NOPROMEDIAR { get; set; }
    }
}
