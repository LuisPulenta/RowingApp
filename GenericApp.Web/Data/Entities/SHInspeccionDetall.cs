using System;
using System.ComponentModel.DataAnnotations;

namespace GenericApp.Web.Data.Entities
{
    public class SHInspeccionDetall
    {
        [Key]
        public int IDREGISTRO { get; set; }
        public int CLAVEINSPECCIONCAB { get; set; }
        public int IDCLIENTE { get; set; }
        public int IDGRUPOFORMULARIO { get; set; }
        public string DETALLEF { get; set; }
        public string DESCRIPCION { get; set; }
        public int PONDERACIONPUNTOS { get; set; }
        public string CUMPLE { get; set; }
    }
}