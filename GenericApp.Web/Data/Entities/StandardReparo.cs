using System.ComponentModel.DataAnnotations;

namespace GenericApp.Web.Data.Entities
{
    public class StandardReparo
    {
        [Key]
        public int CODIGOSTD { get; set; }
        public string DESCRIPCIONTAREA { get; set; }
    }
}