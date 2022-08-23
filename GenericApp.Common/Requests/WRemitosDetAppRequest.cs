namespace GenericApp.Common.Requests
{
    public class WRemitosDetAppRequest
    {
        public int NROREMITODET { get; set; }
        public int NROREMITOCAB { get; set; }
        public int NROOBRA { get; set; }
        public string catCodigo { get; set; }
        public string catCatalogo { get; set; }
        public string CodigoSap { get; set; }
        public decimal Cantidad { get; set; }
        public int? TAG { get; set; }
        public decimal? COSTOUNIT { get; set; }
        public decimal? COSTOTOTAL { get; set; }
    }
}
