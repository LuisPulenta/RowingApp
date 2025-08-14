using System;
using System.ComponentModel.DataAnnotations;

namespace RowingApp.Web.Data.Entities
{
    public class ObrasReparo
    {
        [Key]
        public int NROREGISTRO { get; set; }
        public int NROOBRA { get; set; }
        public DateTime? FECHAALTA { get; set; }
        public DateTime? FECHAINICIO { get; set; }
        public DateTime? FECHACUMPLIMENTO { get; set; }
        public string REQUERIDOPOR { get; set; }
        public string SUBCONTRATISTA { get; set; }
        public string SUBCONTRATISTAREPARO { get; set; }
        public string CODCAUSANTE { get; set; }
        public string NROCTOC { get; set; }
        public string DIRECCION { get; set; }
        public string ALTURA { get; set; }
        public string LATITUD { get; set; }
        public string LONGITUD { get; set; }
        public int? CODTIPOSTDRPARO { get; set; }
        public string ESTADOSUBCON { get; set; }
        public string RECURSOS { get; set; }
        public decimal? MONTODISPONIBLE { get; set; }
        public string GRUA { get; set; }
        public int? IDUsuario { get; set; }
        public string Terminal { get; set; }
        public string Observaciones { get; set; }
        public string Foto1 { get; set; }
        public string TipoVereda { get; set; }
        public int? CantidadMTL { get; set; }
        public int? Ancho { get; set; }
        public int? Profundidad { get; set; }
        public DateTime? FechaCierreElectrico { get; set; }
        public string FotoInicio { get; set; }
        public string FotoFin { get; set; }
        public string Modulo { get; set; }
        public string ObservacionesFotoInicio { get; set; }
        public string ObservacionesFotoFin { get; set; }
        public int? Largo2 { get; set; }
        public int? Ancho2 { get; set; }
        public string ImageFullPath => string.IsNullOrEmpty(Foto1)
        ? $"https://gaos2.keypress.com.ar/RowingAppApi/images/ObrasReparos/noimage.png"
        : $"https://gaos2.keypress.com.ar/RowingAppApi{Foto1.Substring(1)}";

        public string FotoInicioFullPath => string.IsNullOrEmpty(FotoInicio)
       ? $"https://gaos2.keypress.com.ar/RowingAppApi/images/ObrasReparos/noimage.png"
       : $"https://gaos2.keypress.com.ar/RowingAppApi{FotoInicio.Substring(1)}";

        public string FotoFinFullPath => string.IsNullOrEmpty(FotoFin)
       ? $"https://gaos2.keypress.com.ar/RowingAppApi/images/ObrasReparos/noimage.png"
       : $"https://gaos2.keypress.com.ar/RowingAppApi{FotoFin.Substring(1)}";
    }
}