using System;
using System.ComponentModel.DataAnnotations;

namespace RowingApp.Web.Data.Entities
{
    public class ElemenEnCalleVist
    {
        [Key]
        public int ID { get; set; }
        public int IDELEMENTOCAB { get; set; }
        public int NROOBRA   { get; set; }
        public string NombreObra { get; set; }
        public int IDUSERCARGA { get; set; }
        public string NombreCarga { get; set; }
        public string ApellidoCarga { get; set; }
        public DateTime FechaCarga { get; set; }
        public string GRXX { get; set; }
        public string GRYY { get; set; }
        public string DOMICILIO { get; set; }
        public string OBSERVACION { get; set; }
        public string LINKFOTO { get; set; }
        public string ESTADO { get; set; }
        public int? IDUSERRECUPERA { get; set; }
        public string NombreRecupera { get; set; }
        public string ApellidoRecupera { get; set; }
        public DateTime? FECHARECUPERO { get; set; }
        public string CATSIAG { get; set; }
        public string CATSAP { get; set; }
        public string Elemento { get; set; }
        public decimal CANTDEJADA { get; set; }
        public decimal CANTRECUPERADA { get; set; }
        public decimal CantPend { get; set; }
        public string ImageFullPath => string.IsNullOrEmpty(LINKFOTO)
       ? $"http://190.111.249.225/RowingAppApi/images/ElemEnCalle/noimage.png"
       : $"http://190.111.249.225/RowingAppApi{LINKFOTO.Substring(1)}";
    }
}
