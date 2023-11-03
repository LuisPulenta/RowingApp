using System.ComponentModel.DataAnnotations;

namespace GenericApp.Web.Data.Entities
{
    public class ObraEstado
    {
        [Key]
        public string CODIGO { get; set; }
        public string DESCRIPCION { get; set; }
     }
}