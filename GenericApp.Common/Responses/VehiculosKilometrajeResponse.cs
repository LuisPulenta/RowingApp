using System;

namespace GenericApp.Common.Responses
{
    public class VehiculosKilometrajeResponse
    {
        public int Orden { get; set; }
        public DateTime Fecha { get; set; }
        public string Equipo { get; set; }
        public int? KILINI { get; set; }
        public int? KILFIN { get; set; }
        public int? HORSAL { get; set; }
        public int? HORLLE { get; set; }
        public Byte? CODSUC { get; set; }
        public int? NRODEOT { get; set; }
        public string CAMBIO { get; set; }
        public Byte? PROCESADO { get; set; }
        public DateTime? KMFECHAANTERIOR { get; set; }
        public int? NOPROMEDIAR { get; set; }
        public DateTime FECHAALTA { get; set; }
    }
}
