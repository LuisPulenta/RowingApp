using System;
using System.ComponentModel.DataAnnotations;

namespace GenericApp.Web.Data.Entities
{
    public class VFlotaPreventivo
    {
        [Key]
        public int NroInterno { get; set; }
        public string NUMCHA { get; set; }
        public string DescripcionParte { get; set; }
        public string Frecuencia { get; set; }
        public int? CantFrec { get; set; }
        public string Descripcion { get; set; }
        public DateTime? UltFechaEJ { get; set; }
        public int? UltKmHsEj { get; set; }
        public int? ActKmHsEj { get; set; }
        public int? Diferencia { get; set; }
        public string ESTADOS { get; set; }
    }
}