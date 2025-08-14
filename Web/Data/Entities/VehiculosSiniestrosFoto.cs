using System.ComponentModel.DataAnnotations;

namespace RowingApp.Web.Data.Entities
{
    public class VehiculosSiniestrosFoto
    {
        [Key]
        public int IDFOTOSINIESTRO { get; set; }
        public int NROSINIESTROCAB { get; set; }
        public string OBSERVACION { get; set; }
        public string LINKFOTO { get; set; }
        public string CORRESPONDEA { get; set; }
        public string ImageFullPath => string.IsNullOrEmpty(LINKFOTO)
        ? $"https://gaos2.keypress.com.ar/RowingAppApi/images/Siniestros/noimage.png"
        : $"https://gaos2.keypress.com.ar/RowingAppApi{LINKFOTO.Substring(1)}";
    }
}