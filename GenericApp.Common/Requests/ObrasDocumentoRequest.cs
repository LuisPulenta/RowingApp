using System;

namespace GenericApp.Common.Requests
{
    public class ObrasDocumentoRequest
    {
        public int NROOBRA { get; set; }
        public string OBSERVACION { get; set; }
        public byte[] PhotoArray { get; set; }
        public DateTime FECHA { get; set; }
        public string MODULO { get; set; }
        public string NroLote { get; set; }
        public string Sector { get; set; }
        public string Estante { get; set; }
        public string GeneradoPor { get; set; }
    }
}
