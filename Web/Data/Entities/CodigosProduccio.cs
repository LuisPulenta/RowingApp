using System;
using System.ComponentModel.DataAnnotations;

namespace RowingApp.Web.Data.Entities
{
    public class CodigosProduccio
    {
        [Key]
        public string CODIGO     { get; set; }
        public DateTime FECHAMINIMA { get; set; }
        public DateTime FECHAMAXIMA { get; set; }
        public string QPOSTPERMITIDO { get; set; }
        public string QANTPERMITIDO { get; set; }

    }
}
