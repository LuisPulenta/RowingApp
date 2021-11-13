using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GenericApp.Web.Data.Entities
{
    public class ObrasPoste
    {
        [Key]
        public int NROREGISTRO { get; set; }
        public int NROOBRA { get; set; }
        public string ASTICKET { get; set; }
        public string Cliente { get; set; }
        public string DIRECCION { get; set; }
        public string NUMERACION { get; set; }
        public string Localidad { get; set; }
        public string Telefono { get; set; }
        public string TipoImput { get; set; }
        public string CERTIFICADO { get; set; }

        public string SerieMedidorColocado { get; set; }
        public string Precinto { get; set; }
        public string CajaDAE { get; set; }
        public string OBSERVACIONES { get; set; }
        public string Lindero1 { get; set; }
        public string Lindero2 { get; set; }

        public ICollection<ObrasDocumento> ObrasDocumentos { get; set; }
    }
}