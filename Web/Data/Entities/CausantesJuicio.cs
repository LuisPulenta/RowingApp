using System;
using System.ComponentModel.DataAnnotations;

namespace RowingApp.Web.Data.Entities
{
    public class CausantesJuicio
    {
        [Key]
        public int ID_CASO { get; set; }
        public string TIPOCASO { get; set; }
        public string ESTADO { get; set; }
        public DateTime? FECHA_INICIO { get; set; }
        public DateTime? FE_ULT_MOV { get; set; }
        public int? CERRADO { get; set; }
        public DateTime? FECHA_CIERRE { get; set; }
        public string CARATULA { get; set; }
        public string CLIENTE { get; set; }
        public string ABOGADO { get; set; }
        public string JUZGADO { get; set; }
        public string ESCRIBANO { get; set; }
        public string JUEZ   { get; set; }
        public decimal? IMPORTEJUICIO { get; set; }
        public string MONEDA { get; set; }
        public decimal? IMPORTEREALDEUDA { get; set; }
        public DateTime? FECHA_VENCIMIENTO { get; set; }
        public DateTime? FECHACALCULO { get; set; }
        public int? DIASATRASO { get; set; }
        public decimal? INTERESES_MORATORIOS { get; set; }
        public decimal? IMPORTEINTERES { get; set; }
        public decimal? INTERESES_PUNITORIOS { get; set; }
        public decimal? IMPORTEPUNITORIO { get; set; }
        public decimal? GASTOS_JUDICIALES { get; set; }
        public decimal? IMPORTEGASTOS { get; set; }
        public int? COBROSCLIENTE { get; set; }
        public decimal? PAGOSDEMANDADO { get; set; }
        public decimal? HONORARIOS { get; set; }
        public decimal? IMPORTEHONORARIOS { get; set; }
        public int? VARIOS { get; set; }
        public decimal? IMPORTEVARIOS { get; set; }
        public int? PERIODO { get; set; }
        public string GRUPO { get; set; }
        public string CAUSANTE { get; set; }
        public int? IDUSERIMPUT { get; set; }
        public string NroExpediente { get; set; }
        public int? IDContraparte { get; set; }
    }
}
