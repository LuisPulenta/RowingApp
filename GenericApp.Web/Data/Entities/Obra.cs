using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace GenericApp.Web.Data.Entities
{
    public class Obra
    {
        [Key]
        public int NroObra { get; set; }
        public string NombreObra { get; set; }
        public string ELEMPEP { get; set; }
        public string OBSERVACIONES { get; set; }
        public int Finalizada { get; set; }
        public int ULTIMAACTA { get; set; }
        public string SUPERVISORE { get; set; }
        public string CodigoEstado { get; set; }
        public string Modulo { get; set; }
        public string GrupoAlmacen { get; set; }
        public string GrupoCausante { get; set; }
        public ICollection<ObrasDocumento> ObrasDocumentos { get; set; }
        public int HabilitaReclamosAPP { get; set; }
        public int? CORRESPONDEABONADOS { get; set; }
        public DateTime? FechaCierreElectrico { get; set; }
        public DateTime? FechaUltimoMovimiento { get; set; }
        public int Photos => ObrasDocumentos == null ? 0 : ObrasDocumentos.Count(e => e.TipoDeFoto < 20);
        public int Audios => ObrasDocumentos == null ? 0 : ObrasDocumentos.Count(e => e.TipoDeFoto == 20);
        public int Videos => ObrasDocumentos == null ? 0 : ObrasDocumentos.Count(e => e.TipoDeFoto == 30);
    }
}