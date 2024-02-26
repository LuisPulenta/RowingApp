using System;
using System.ComponentModel.DataAnnotations;

namespace GenericApp.Web.Data.Entities
{
    public class Movimiento
    {
        [Key]
        public int NroMovimiento { get; set; }
        public DateTime FechaCarga { get; set; }
        public string CodigoConcepto { get; set; }
        public string CodigoGrupo { get; set; }
        public string CodigoCausante { get; set; }
        public string CodigoGrupoRec { get; set; }
        public string CodigoCausanteRec { get; set; }
        public int NroRemitoR { get; set; }
        public string DocSAP { get; set; }
        public int? NroLote { get; set; }
        public int UsrAlta { get; set; }
        public string LinkRemito { get; set; }
        public int Recibido { get; set; }
    }
}
