using System.ComponentModel.DataAnnotations;

namespace GenericApp.Web.Data.Entities
{
    public class AppInstalacionesEquiposDetalle
    {
        [Key]
        public int IDDETALLE { get; set; }
        public int IDINSTALACIONEQUIPO { get; set; }
        public string NROSERIEINSTALADA { get; set; }
        public int IDLOTECAB { get; set; }
        public string CODSIAG { get; set; }
        public string CODSAP { get; set; }

    }
}
