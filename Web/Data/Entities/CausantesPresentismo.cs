using System;
using System.ComponentModel.DataAnnotations;

namespace RowingApp.Web.Data.Entities
{
    public class CausantesPresentismo
    {
        [Key]
        public int IDPRESENTISMO { get; set; }
        public int IDSUPERVISOR { get; set; }
        public DateTime FECHA { get; set; }
        public int HORA { get; set; }
        public string GRUPOC { get; set; }
        public string CAUSANTEC { get; set; }
        public string ESTADO { get; set; }
        public string ZONATRABAJO { get; set; }
        public string ACTIVIDAD { get; set; }
        public string CECO { get; set; }
        public string OBSERVACIONES { get; set; }
        public string PerteneceCuadrilla { get; set; }
    }
}
