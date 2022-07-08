using System.ComponentModel.DataAnnotations;

namespace GenericApp.Web.Data.Entities
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
        ? $"http://190.111.249.225/RowingAppApi/images/Siniestros/noimage.png"
        : $"http://190.111.249.225/RowingAppApi{LINKFOTO.Substring(1)}";
    }
}