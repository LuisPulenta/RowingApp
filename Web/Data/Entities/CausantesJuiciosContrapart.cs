using System.ComponentModel.DataAnnotations;
namespace RowingApp.Web.Data.Entities
{
    public class CausantesJuiciosContrapart
    {
        [Key]
        public int IDCONTRAPARTE { get; set; }
        public string APELLIDONOMBRE { get; set; }
        public string EMAIL { get; set; }
        public string TELEFONO { get; set; }
        public string CELULAR { get; set; }
        public string DOMICILIOESTUDIO { get; set; }
        public string OBSERVACIONES { get; set; }
    }
}
