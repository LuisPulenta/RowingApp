using System.ComponentModel.DataAnnotations;

namespace GenericApp.Web.Data.Entities
{
    public class CausantesEstado
    {
        [Key]
        public string NOMENCLADORESTADO { get; set; }
        public int SoloAPP { get; set; }
            }
}
