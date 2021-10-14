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
    }
}
