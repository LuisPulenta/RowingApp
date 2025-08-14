using System.ComponentModel.DataAnnotations;

namespace RowingApp.Web.Data.Entities
{
    public class CausantesObra
    {
        [Key]
        public int NroCausante { get; set; }
        public string grupo { get; set; }
        public string codigo { get; set; }
        public string nombre { get; set; }
        public bool estado { get; set; }
    }
}
