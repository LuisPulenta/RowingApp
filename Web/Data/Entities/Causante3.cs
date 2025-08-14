using System.ComponentModel.DataAnnotations;
namespace RowingApp.Web.Data.Entities
{
    public class Causante3
    {
        [Key]
        public int NroCausante { get; set; }
        public string codigo { get; set; }
        public string nombre { get; set; }
        public string email { get; set; }
        public string telefono { get; set; }
        public string grupo { get; set; }
        public string NroSAP { get; set; }
        public bool estado { get; set; }
        public int? HabilitaReciboSueldoApp { get; set; }
    }
}
