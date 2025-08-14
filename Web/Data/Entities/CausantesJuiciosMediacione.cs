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
       ? $"https://gaos2.keypress.com.ar/RowingAppApi/images/Legales/noimage.png"
       : $"https://gaos2.keypress.com.ar/RowingAppApi{LINKARCHIVOMEDIACION.Substring(1)}";
    }
}
