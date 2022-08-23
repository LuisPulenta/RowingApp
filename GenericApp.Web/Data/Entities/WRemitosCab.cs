using System;
using System.ComponentModel.DataAnnotations;

namespace GenericApp.Web.Data.Entities
{
    public class WRemitosCab
    {
        [Key]
        public int NROREMITO { get; set; }
        public int NROOBRA { get; set; }
        public int? NroReserva { get; set; }
        public string NroGrafo { get; set; }
        public DateTime FECHACARGA { get; set; }
        public string CONTRATISTA { get; set; }
        public int IDUSUARIO { get; set; }
        public int? CONFIRMAGENERADO { get; set; }
        public int? CONFIRMAENTREGADO { get; set; }
        public int? ANULADO { get; set; }
        public string TIPO { get; set; }
        public string RETIRA { get; set; }
        public string CLASIFICACION { get; set; }
        public string OBSERVACION { get; set; }
        public string CODGRUPOC { get; set; }
        public string CODCAUSANTEC { get; set; }
        public string CODGRUPOREC { get; set; }
        public string CODCAUSANTEREC { get; set; }
        public string CODCONCEPTO { get; set; }
        public DateTime? FECHARETIRO { get; set; }
        public string PRIORIDAD { get; set; }
        public int? FALTAMATERIAL { get; set; }
        public int? DESPACHADO { get; set; }
        public string TERMINAL { get; set; }
        public int? PORDIFERENCIA { get; set; }
        public string ENTREGADOCONTRATISTA { get; set; }
        public string MODULO { get; set; }
        public int? COBRADO602 { get; set; }
        public int? NROOP { get; set; }
        public decimal? VALORIZACION { get; set; }
    }
}

