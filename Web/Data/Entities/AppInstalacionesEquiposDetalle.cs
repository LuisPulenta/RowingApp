using System.ComponentModel.DataAnnotations;

namespace RowingApp.Web.Data.Entities
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
        public string NombreEquipo { get; set; }
        public string LinkFoto { get; set; }
        public int NROREGISTROLOTESCAB { get; set; }
        public string Familia { get; set; }
        public string ImageFullPath => string.IsNullOrEmpty(LinkFoto)
        ? $"https://gaos2.keypress.com.ar/RowingAppApi/images/Instalaciones/noimage.png"
        : $"https://gaos2.keypress.com.ar/RowingAppApi{LinkFoto.Substring(1)}";
    }
}
