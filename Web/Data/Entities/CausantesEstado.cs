using System.ComponentModel.DataAnnotations;

namespace RowingApp.Web.Data.Entities
{
    public class CausantesEstado
    {
        [Key]
        public string NOMENCLADORESTADO { get; set; }
        public int SoloAPP { get; set; }
            }
}
