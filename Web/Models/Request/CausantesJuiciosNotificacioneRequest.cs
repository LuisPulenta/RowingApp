using System;

namespace RowingApp.Common.Requests
{
    public class CausantesJuiciosNotificacioneRequest
    {
        public int IDNOTIFICACION { get; set; }
        public int IDJUICIO { get; set; }
        public string TIPO { get; set; }
        public string TITULO { get; set; }
        public string OBSERVACIONES { get; set; }
        public string MONEDA { get; set; }
        public decimal? MONTO { get; set; }
        public string TIPOTRANSACCION { get; set; }
        public string CONDICIONPAGO { get; set; }
        public string NROFACTURA { get; set; }
        public string LUGAR { get; set; }
        public string PARTICIPANTES { get; set; }
        public DateTime? FECHAECHO { get; set; }
        public DateTime? FECHAVENCIMIENTO { get; set; }
        public string TIPOARRAY { get; set; }
        public byte[] ImageArray { get; set; }
    }
}
