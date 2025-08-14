using System;

namespace RowingApp.Common.Requests
{
    public class CausantesJuiciosMediacioneRequest
    {
        public int IDMEDIACION { get; set; }
        public int IDCAUSANTEJUICIO { get; set; }
        public string MEDIADORES { get; set; }
        public DateTime? FECHA { get; set; }
        public string ABOGADO { get; set; }
        public int? IDCONTRAPARTE { get; set; }
        public string MONEDA { get; set; }
        public decimal? OFRECIMIENTO { get; set; }
        public string TIPOTRANSACCION { get; set; }
        public string CONDICIONPAGO { get; set; }
        public DateTime? VENCIMIENTOOFERTA { get; set; }
        public string RESULTADOOFERTA { get; set; }
        public decimal? MONTOCONTRAOFERTA { get; set; }
        public string ACEPTACIONCONTRAOFERTA { get; set; }
        public string TIPOARRAY { get; set; }
        public byte[] ImageArray { get; set; }
    }
}
