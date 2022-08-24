using System.ComponentModel.DataAnnotations;

namespace GenericApp.Web.Data.Entities
{
    public class WRemitosDet
    {
        [Key]
        public int NROREMITODET { get; set; }
        public int NROREMITOCAB { get; set; }
        public int NROOBRA { get; set; }
        public string catCodigo { get; set; }
        public string catCatalogo { get; set; }
        public string CodigoSap { get; set; }
        public decimal Cantidad { get; set; }
        public string OBSERVACION { get; set; }
        public int? NRORESERVA { get; set; }
        public string NROGRAFO { get; set; }
        public int? TAG { get; set; }
        public decimal? COSTOUNIT { get; set; }
        public decimal? COSTOTOTAL { get; set; }
    }
}