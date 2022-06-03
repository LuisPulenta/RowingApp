using System;

namespace GenericApp.Common.Requests
{
    public class CausantesNovedadeRequest
    {
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
        public string LinkAdjunto1 { get; set; }
        public string LinkAdjunto2 { get; set; }
        public byte[] ImageArray1 { get; set; }
        public byte[] ImageArray2 { get; set; }
        public DateTime? FechaEstado { get; set; }
        public string ObservacionEstado { get; set; }
        public int? ConfirmaLeido { get; set; }
        public int? IDUsrEstado { get; set; }
        public string Estado { get; set; }
    }
}
