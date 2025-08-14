using System.ComponentModel.DataAnnotations;

namespace RowingApp.Web.Data.Entities
{
    public class ElementosEnCalleDet    
    {
        [Key]
        public int ID { get; set; }
        public int IDELEMENTOCAB { get; set; }
        public string CATSIAG { get; set; }
        public string CATSAP { get; set; }
        public decimal CANTDEJADA { get; set; }
        public decimal CANTRECUPERADA { get; set; }
    }
}
