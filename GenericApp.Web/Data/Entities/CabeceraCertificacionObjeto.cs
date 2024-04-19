using System.ComponentModel.DataAnnotations;

namespace GenericApp.Web.Data.Entities
{
    public class CabeceraCertificacionObjeto
    {
        [Key]
        public string OBJETOS { get; set; }
        public string Modulo { get; set; }
    }
}
