using System.ComponentModel.DataAnnotations;

namespace GenericApp.Web.Data.Entities
{
    public class Subcontratista
    {
        [Key]   
        public string subCodigo { get; set; }
        public string subSubcontratista { get; set; }
        public string MODULO { get; set; }
        public byte? subDeshabilitado { get; set; }
        
    }
}