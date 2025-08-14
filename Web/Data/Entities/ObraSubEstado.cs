using System.ComponentModel.DataAnnotations;

namespace RowingApp.Web.Data.Entities
{
    public class ObraSubEstado
    {
        [Key]
        public string CODIGOSUBESTADO { get; set; }
        public string CODIGOESTADO { get; set; }
        public string DESCRIPCION { get; set; }
     }
}