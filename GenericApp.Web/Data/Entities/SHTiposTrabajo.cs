using System.ComponentModel.DataAnnotations;

namespace GenericApp.Web.Data.Entities
{
    public class SHTiposTrabajo
    {
        [Key]
        public int IDREGISTRO { get; set; }
        public int IDTIPOTRABAJO { get; set; }
        public string DESCRIPCION { get; set; }
        public int IDCLIENTE { get; set; }
    }
}