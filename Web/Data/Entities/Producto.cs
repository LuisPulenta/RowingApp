using System.ComponentModel.DataAnnotations;

namespace RowingApp.Web.Data.Entities
{
    public class Producto
    {
        [Key]
        public string CodProducto { get; set; }
        public string CodigoSAP { get; set; }
        public string Denominacion { get; set; }
        public decimal Dep2 { get; set; }        
    }
}
