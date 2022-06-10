using System.ComponentModel.DataAnnotations;

namespace GenericApp.Web.Data.Entities
{
    public class Causante
    {
        [Key]
        public int NroCausante { get; set; }
        public string codigo { get; set; }
        public string nombre { get; set; }
        public string encargado { get; set; }
        public string telefono { get; set; }
        public string grupo { get; set; }
        public string NroSAP { get; set; }
        public bool estado { get; set; }
        public string RazonSocial { get; set; }
        public string LinkFoto { get; set; }
        public string ImageFullPath => string.IsNullOrEmpty(LinkFoto)
        ? $"http://190.111.249.225/RowingAppApi/images/Causantes/nouser.png"
        : $"http://190.111.249.225/RowingAppApi{LinkFoto.Substring(1)}";
    }
}
