using System;
using System.ComponentModel.DataAnnotations;

namespace GenericApp.Web.Data.Entities
{
    public class VehiculosProgramaPrev
    {
        [Key]
        public int NroInterno { get; set; }
        public string CodigoDePrograma { get; set; }
        public string CodigoDeEquipo { get; set; }
        public int? CodigoDeParte { get; set; }
        public string CodigoDeTarea  { get; set; }
        public int? CantFrec { get; set; }
        public DateTime? UltimaLectura { get; set; }
        public DateTime? EJECUCION { get; set; }
        public int? FrecDias { get; set; }
        public int? Contador { get; set; }
        public string Actualizado { get; set; }
        public int? KMDesdeUltimaVerificacion { get; set; }
        public string Frecuencia { get; set; }
        public string ESTADOS { get; set; }
        public int? DIFERENCIA { get; set; }
        public int? PROXIMAREV { get; set; }
    }
}
