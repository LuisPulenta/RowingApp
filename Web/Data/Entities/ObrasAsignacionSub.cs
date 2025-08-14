using System;
using System.ComponentModel.DataAnnotations;

namespace RowingApp.Web.Data.Entities
{
    public class ObrasAsignacionSub
    {
        [Key]
        public int NROREGISTRO { get; set; }
        public int NROOBRA { get; set; }
        public string SUBCONTRATISTA { get; set; }
        public string CAUSANTE { get; set; }
        public string TAREAQUEREALIZA { get; set; }
        public string OBSERVACION { get; set; }
        public DateTime? FECHAALTA { get; set; }
        public DateTime? FECHAFINASIGNACION { get; set; }
        public int IDUSR { get; set; }
        public DateTime? FechaCierre { get; set; }
    }
}