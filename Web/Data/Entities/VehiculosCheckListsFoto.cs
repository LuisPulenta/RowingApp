using System;
using System.ComponentModel.DataAnnotations;

namespace RowingApp.Web.Data.Entities
{
    public class VehiculosCheckListsFoto
    {
        [Key]
        public int IDREGISTRO { get; set; }
        public int IDCHECKLISTCAB { get; set; }
        public string DESCRIPCION { get; set; }
        public string LINKFOTO { get; set; }
        public string ImageFullPath => string.IsNullOrEmpty(LINKFOTO)
        ? $"https://gaos2.keypress.com.ar/RowingAppApi/images/CheckList/noimage.png"
        : $"https://gaos2.keypress.com.ar/RowingAppApi{LINKFOTO.Substring(1)}";
    }
}