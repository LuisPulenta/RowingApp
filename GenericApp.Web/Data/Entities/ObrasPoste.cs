using System;
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
        public int IDActaCertif { get; set; }
        public string OBSERVACIONES { get; set; }
        public string Lindero1 { get; set; }
        public string Lindero2 { get; set; }

        public string ZONA { get; set; }
        public string TERMINAL { get; set; }
        public string Subcontratista { get; set; }
        public string CausanteC { get; set; }

        public string GRXX { get; set; }
        public string GRYY { get; set; }
        public int IDUsrIn { get; set; }
        public string ObservacionAdicional { get; set; }
        public DateTime FechaCarga { get; set; }
        public string RiesgoElectrico { get; set; }
        public DateTime FECHAASIGNACION { get; set; }
        public DateTime? FechaActualizada { get; set; }
        public int? IDUsrAcualiza { get; set; }
        public string ProvieneAct { get; set; }


        //public ICollection<ObrasDocumento> ObrasDocumentos { get; set; }
    }
}