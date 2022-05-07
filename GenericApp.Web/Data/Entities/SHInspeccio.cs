using System;
using System.ComponentModel.DataAnnotations;

namespace GenericApp.Web.Data.Entities
{
    public class SHInspeccio
    {
        [Key]
        public int IDINSPECCION { get; set; }
        public int IDCLIENTE { get; set; }
        public DateTime FECHA { get; set; }
        public int USUARIOALTA { get; set; }
        public string LATITUD { get; set; }
        public string LONGITUD { get; set; }
        public int IDOBRA { get; set; }
        public string SUPERVISOR { get; set; }
        public string VEHICULO { get; set; }
        public int NROLEGAJO { get; set; }
        public string GRUPOC { get; set; }
        public string CAUSANTEC { get; set; }
        public string DNI { get; set; }
        public int ESTADO { get; set; }
        public string OBSERVACIONESINSPECCION { get; set; }
        public string AVISO { get; set; }
        public int EMAILENVIADO { get; set; }
        public int REQUIEREREINSPECCION { get; set; }


    }
}