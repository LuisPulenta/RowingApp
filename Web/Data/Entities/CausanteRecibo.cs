using System;
using System.ComponentModel.DataAnnotations;

namespace RowingApp.Web.Data.Entities
{
    public class CausanteRecibo
    {
        [Key]
        public int IDRECIBO { get; set; }
        public string GRUPO { get; set; }
        public string CAUSANTE { get; set; }
        public int MES { get; set; }
        public int ANIO { get; set; }
        public string LINK { get; set; }
        public int? FIRMADO { get; set; }
        public DateTime? FECHACARGA { get; set; }
        public string EstadoRecibo { get; set; }
        public DateTime? FECHAESTADO { get; set; }
        public int? HSESTADO { get; set; }
        public string OBSERVACIONES { get; set; }
        public string IMEI { get; set; }
        public DateTime? FECHAFIRMAELECTRONICA { get; set; }
        public int? HSFIRMAELECTRONICA { get; set; }
        public string LATFIRMAELECTRONICA { get; set; }
        public string LONGFIRMAELECTRONICA { get; set; }
        public string PeriodoCalcNomina { get; set; }
        public string NroSecuencia { get; set; }
        public string Periodo { get; set; }
        public string PDFFromExcel { get; set; }
        public DateTime? FechaPagoExcel { get; set; }
        public DateTime? FechaIniExcel { get; set; }
        public DateTime? FechaFinExcel { get; set; }
    }
}
