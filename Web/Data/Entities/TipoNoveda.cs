using System.ComponentModel.DataAnnotations;

namespace RowingApp.Web.Data.Entities
{
    public class TipoNoveda
    {
        [Key]
        public string TIPODENOVEDAD { get; set; }
    }
}