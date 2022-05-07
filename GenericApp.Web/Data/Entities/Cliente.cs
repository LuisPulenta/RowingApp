using System.ComponentModel.DataAnnotations;

namespace GenericApp.Web.Data.Entities
{
    public class Cliente    
    {
        [Key]
        public int NROCLIENTE { get; set; }
        public string NOMBRE { get; set; }
    }
}