        using System;

namespace RowingApp.Common.Requests
{
    public class ObrasReparosRequest
    {

        public int NROREGISTRO { get; set; }
        public int NROOBRA { get; set; }
        public string DIRECCION { get; set; }
        public string ALTURA { get; set; }
        public string LATITUD { get; set; }
        public string LONGITUD { get; set; }
        public int? IDUsuario { get; set; }
        public string Observaciones { get; set; }
        public string TipoVereda { get; set; }
        public int? CantidadMTL { get; set; }
        public int? Ancho { get; set; }
        public int? Profundidad { get; set; }
        public DateTime? FechaCierreElectrico { get; set; }
        public byte[] ImageArray { get; set; }
        public byte[] FotoInicioArray { get; set; }
        public byte[] FotoFinArray { get; set; }
        public int? CODTIPOSTDRPARO { get; set; }
        public string Modulo { get; set; }
        public string ObservacionesFotoInicio { get; set; }
        public string ObservacionesFotoFin { get; set; }
        public int? Largo2 { get; set; }
        public int? Ancho2 { get; set; }

    }
}
