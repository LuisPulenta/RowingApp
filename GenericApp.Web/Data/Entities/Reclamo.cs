using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GenericApp.Web.Data.Entities
{
    public class Reclamo
    {
        [Key]
        public int NROREGISTRO { get; set; }
        public int NROOBRA { get; set; }
        public string ASTICKET { get; set; }
        public string ZONA { get; set; }
        public string TERMINAL { get; set; }
        public string DIRECCION { get; set; }
        public string NUMERACION { get; set; }
        public string CODIGOGRUPO { get; set; }
        public string CODIGOCAUSANTE { get; set; }
        public string TipoImput { get; set; }
    }
}