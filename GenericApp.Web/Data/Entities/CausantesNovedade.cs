using System;
using System.ComponentModel.DataAnnotations;

namespace GenericApp.Web.Data.Entities
{
    public class CausantesNovedade
    {
        [Key]
        public int IDNOVEDAD { get; set; }
        public string GRUPO { get; set; }
        public string CAUSANTE { get; set; }
        public DateTime FECHACARGA { get; set; }
        public DateTime FECHANOVEDAD { get; set; }
        public string EMPRESA { get; set; }
        public DateTime FECHAINICIO { get; set; }
        public DateTime FECHAFIN { get; set; }
        public string TIPONOVEDAD { get; set; }
        public string OBSERVACIONES { get; set; }
        public int VistaRRHH { get; set; }
        public int Idusuario { get; set; }
    }
}