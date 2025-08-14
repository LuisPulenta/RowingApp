using System.ComponentModel.DataAnnotations;

namespace RowingApp.Web.Data.Entities
{
    public class Parametro
    {
        [Key]
        public int ID { get; set; }
        public byte BLOQUEAACTAS { get; set; }
        public string IPServ { get; set; }
        public int Metros { get; set; }
        public int Tiempo { get; set; }
        public int APPBloqueada { get; set; }        
    }        
}
