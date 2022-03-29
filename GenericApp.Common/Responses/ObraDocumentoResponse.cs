using System;

namespace GenericApp.Common.Responses
{
    public class ObraDocumentoResponse
    {
        public int NROREGISTRO { get; set; }
        public int NROOBRA { get; set; }
        public int? IDObrasPostes { get; set; }
        public string OBSERVACION { get; set; }
        public string LINK { get; set; }
        public DateTime? FECHA { get; set; }
        public string MODULO { get; set; }
        public string NroLote { get; set; }
        public string Sector { get; set; }
        public string Estante { get; set; }
        public string GeneradoPor { get; set; }
        public double? Latitud { get; set; }
        public double? Longitud { get; set; }
        public DateTime? FechaHsFoto { get; set; }
        public int? TipoDeFoto { get; set; }
        public string DireccionFoto { get; set; }
        public string ImageFullPath => string.IsNullOrEmpty(LINK)
        ? $"http://190.111.249.225/RowingAppApi/images/Obras/noimage.png"
        : $"http://190.111.249.225/RowingAppApi{LINK.Substring(1)}";

        public string DescFoto => TipoDeFoto == 0
        ? "Relevamiento(Vereda/Calzada/Traza)"
        : TipoDeFoto == 1
            ? "Previa al Trabajo"
            : TipoDeFoto == 2
            ? "Durante el Trabajo"
            : TipoDeFoto == 3
            ? "Finalización del Trabajo"
            : TipoDeFoto == 4
            ? "N° de Medidor Colocado"
            : TipoDeFoto == 5
            ? "Estado de medidor retirado"
            : TipoDeFoto == 6
            ? "N° de Precinto"
            : TipoDeFoto == 7
            ? "N° de tapa o caja"
            : TipoDeFoto == 8
            ? "Lindero 1"
            : TipoDeFoto == 9
            ? "Lindero 2"
            :"";
    }
}
