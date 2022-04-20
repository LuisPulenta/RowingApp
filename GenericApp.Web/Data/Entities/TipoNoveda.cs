using System.ComponentModel.DataAnnotations;

namespace GenericApp.Web.Data.Entities
{
    public class TipoNoveda
    {
        [Key]
        public string TIPODENOVEDAD { get; set; }
    }
}