using System;
using System.ComponentModel.DataAnnotations;

namespace RowingApp.Web.Data.Entities
{
    public class ConteoCiclicoCa
    {
        [Key]
        public int IDREGISTRO { get; set; }
        public int IDUserCarga { get; set; }
        public int IdUserAsignado { get; set; }
        public DateTime FechaCarga { get; set; }
        public string GrupoD { get; set; }
        public string CausanteD { get; set; }
        public string Observacion { get; set; }
        public int Aprobado { get; set; }
        public DateTime? FechaAprobado { get; set; }
        public string Terminal { get; set; }
        public int IdMov901 { get; set; }
        public decimal Monto901 { get; set; }
        public int IdMov902 { get; set; }
        public decimal Monto902 { get; set; }
        public int ProcesadoGaos { get; set; }
        public string nombre { get; set; }
    }
}
