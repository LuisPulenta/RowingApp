using GenericApp.Common.Responses;
using System;

namespace GenericApp.Common.Requests
{
    public class ObrasDocumentoRequest
    {
        public int NROREGISTRO { get; set; }
        public int NROOBRA { get; set; }
        public int IDObrasPostes { get; set; }
        public string OBSERVACION { get; set; }
        public byte[] ImageArray { get; set; }
        public string LINK { get; set; }
        public DateTime FECHA { get; set; }
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
        public ObraResponse Obra { get; set; }
    }
}
