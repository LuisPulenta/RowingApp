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
        ? $"http://190.111.249.225/RowingAppApi/images/CheckList/noimage.png"
        : $"http://190.111.249.225/RowingAppApi{LINKFOTO.Substring(1)}";
    }
}