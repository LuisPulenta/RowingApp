using System.ComponentModel.DataAnnotations;
namespace RowingApp.Web.Data.Entities
{
    public class ConteoCiclicoDe
    {
        [Key]
        public int IDCONTEODET { get; set; }
        public int IDCONTEOCAB { get; set; }
        public string CODIGOSIAG { get; set; }
        public string CODIGOSAP { get; set; }
        public string DESCRIPCION { get; set; }
        public decimal CONTEOACTUAL { get; set; }
        public decimal SEGUNINVENTARIO { get; set; }
        public decimal VALORACTUAL { get; set; }
    }
}
