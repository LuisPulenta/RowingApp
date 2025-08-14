    using System;
using System.ComponentModel.DataAnnotations;
namespace RowingApp.Web.Data.Entities
{
    public class CausantesJuiciosMediacione
    {
        [Key]
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
        public string LINKARCHIVOMEDIACION { get; set; }
        public string LINKARCHIVOMEDIACIONFullPath => string.IsNullOrEmpty(LINKARCHIVOMEDIACION)
       ? $"http://190.111.249.225/RowingAppApi/images/Legales/noimage.png"
       : $"http://190.111.249.225/RowingAppApi{LINKARCHIVOMEDIACION.Substring(1)}";
    }
}
