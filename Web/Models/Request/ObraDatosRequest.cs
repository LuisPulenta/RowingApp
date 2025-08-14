using System;
using System.ComponentModel.DataAnnotations;

namespace RowingApp.Common.Requests
{
    public class ObraDatosRequest
    {
        [Required]
        public int NroObra { get; set; }
        public string POSX { get; set; }
        public string POSY { get; set; }
        public string Direccion { get; set; }
        public string TextoLocalizacion { get; set; }
        public string TextoClase { get; set; }
        public string TextoTipo { get; set; }
        public string TextoComponente { get; set; }
        public string CodigoDiametro { get; set; }
        public string Motivo { get; set; }
        public string Planos { get; set; }
    }
}