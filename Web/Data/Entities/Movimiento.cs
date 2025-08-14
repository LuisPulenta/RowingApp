using System;
using System.ComponentModel.DataAnnotations;

namespace RowingApp.Web.Data.Entities
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
        public bool Recibido { get; set; }

        public string ImageFullPath => string.IsNullOrEmpty(LinkRemito)
        ? $"http://190.111.249.225/RowingAppApi/images/Obras/noimage.png"
        : $"http://190.111.249.225/RowingAppApi{LinkRemito.Substring(1)}";
    }
}
