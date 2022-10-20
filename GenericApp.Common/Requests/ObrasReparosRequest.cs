using System;

namespace GenericApp.Common.Requests
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
        public DateTime FechaCierreElectrico { get; set; }
        public byte[] ImageArray { get; set; }
       
    }
}
