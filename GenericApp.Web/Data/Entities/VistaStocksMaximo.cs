using System.ComponentModel.DataAnnotations;

namespace GenericApp.Web.Data.Entities
{
    public class VistaStocksMaximo
    {
        [Key]
        public string Id { get; set; }
        public string GRUPOC { get; set; }
        public string CAUSANTE { get; set; }
        public string CODIGOSIAG { get; set; }
        public string CODIGOSAP { get; set; }
        public string catCatalogo { get; set; }
        public decimal? MAXIMO { get; set; }
    }
}
