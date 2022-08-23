using System;

namespace GenericApp.Common.Requests
{
    public class WRemitosCabAppRequest
    {
        //public int NROREGISTRO { get; set; }
        public int NROOBRA { get; set; }
        public DateTime FECHACARGA { get; set; }
        public string CONTRATISTA { get; set; }
        public int IDUSUARIO { get; set; }
        public string CODGRUPOREC { get; set; }
        public string CODCAUSANTEREC { get; set; }
        public string CODCONCEPTO { get; set; }
        public string PRIORIDAD { get; set; }
        public int? FALTAMATERIAL { get; set; }
        public int? DESPACHADO { get; set; }
        public int? PORDIFERENCIA { get; set; }
        public string ENTREGADOCONTRATISTA { get; set; }
        public string MODULO { get; set; }
        public int? COBRADO602 { get; set; }
        public int? NROOP { get; set; }
        public decimal? VALORIZACION { get; set; }
    }
}