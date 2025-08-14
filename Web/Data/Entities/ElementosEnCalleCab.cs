using System;
using System.ComponentModel.DataAnnotations;

namespace RowingApp.Web.Data.Entities
{
    public class ElementosEnCalleCab
    {
        [Key]
        public int ID { get; set; }
        public int NROOBRA   { get; set; }
        public int IDUSERCARGA { get; set; }
        public DateTime FECHA { get; set; }
        public string GRXX { get; set; }
        public string GRYY { get; set; }
        public string DOMICILIO { get; set; }
        public string OBSERVACION { get; set; }
        public string LINKFOTO { get; set; }
        public string ESTADO { get; set; }
        public int? IDUSERRECUPERA { get; set; }
        public DateTime? FECHARECUPERO { get; set; }
    }
}
