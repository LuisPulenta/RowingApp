using System;
using System.ComponentModel.DataAnnotations;

namespace GenericApp.Web.Data.Entities
{
    public class VistaInspeccione
    {
        [Key]
        public int IDInspeccion { get; set; }
        public int UsuarioAlta { get; set; }
        public DateTime Fecha { get; set; }
        public string Empleado { get; set; }
        public string Cliente { get; set; }
        public string TipoTrabajo { get; set; }
        public string Obra { get; set; }
        public int TotalPreguntas { get; set; }
        public int TotalNo { get; set; }
        public int Puntos { get; set; }
        public string DniSR { get; set; }
        public string NombreSR { get; set; }
        public int IDCliente{ get; set; }
        public int IDTipoTrabajo { get; set; }
    }
}
