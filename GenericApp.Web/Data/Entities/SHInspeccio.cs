using System;
using System.ComponentModel.DataAnnotations;

namespace GenericApp.Web.Data.Entities
{
    public class SHInspeccio
    {
        [Key]
        public int IDInspeccion { get; set; }
        public int IDCliente { get; set; }
        public DateTime Fecha { get; set; }
        public int UsuarioAlta { get; set; }
        public string Latitud { get; set; }
        public string Longitud { get; set; }
        public int IDObra { get; set; }
        public string Supervisor { get; set; }
        public string Vehiculo { get; set; }
        public int NroLegajo { get; set; }
        public string GrupoC { get; set; }
        public string CausanteC { get; set; }
        public string DNI { get; set; }
        public int Estado { get; set; }
        public string ObservacionesInspeccion { get; set; }
        public string Aviso { get; set; }
        public int EmailEnviado { get; set; }
        public int RequiereReinspeccion { get; set; }
        public int TotalPreguntas { get; set; }
        public int RespSi { get; set; }
        public int RespNo { get; set; }
        public int RespNA { get; set; }
        public int TotalPuntos { get; set; }
        public string DniSR { get; set; }
        public string NombreSR { get; set; }
        public int IDTipoTrabajo { get; set; }
    }
}