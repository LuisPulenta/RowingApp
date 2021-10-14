using System;

namespace GenericApp.Common.Responses
{
    public class EntregaDetalleResponse
    {
        public int identity_column { get; set; }
        public string codigo { get; set; }
        public string grupo { get; set; }
        public string causante { get; set; }
        public DateTime fecha { get; set; }
        public string CODIGOSAP { get; set; }
        public string Denominacion { get; set; }
        public decimal stock_act { get; set; }
    }
}
