using System;

namespace GenericApp.Common.Requests
{
    public class VehiculosKilometrajeRequest
    {
        public int Orden { get; set; }
        public DateTime Fecha { get; set; }
        public string Equipo { get; set; }
        public int? KILINI { get; set; }
        public int? KILFIN { get; set; }
        public int? HORSAL { get; set; }
        public int? HORLLE { get; set; }
        public byte? CODSUC { get; set; }
        public int? NRODEOT { get; set; }
        public string CAMBIO { get; set; }
        public byte? PROCESADO { get; set; }
        public DateTime? KMFECHAANTERIOR { get; set; }
        public int? NOPROMEDIAR { get; set; }
        public DateTime FECHAALTA { get; set; }
    }
}
