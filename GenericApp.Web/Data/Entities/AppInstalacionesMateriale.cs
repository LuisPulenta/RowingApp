using System.ComponentModel.DataAnnotations;

namespace GenericApp.Web.Data.Entities
{
    public class AppInstalacionesMateriale
    {
        [Key]
        public int IdMaterial { get; set; }
        public int IdInstalacionEquipo { get; set; }
        public string CodigoSIAG { get; set; }
        public string CodigoSAP { get; set; }
        public string Descripcion { get; set; }
        public float Cantidad { get; set; }        
    }
}
