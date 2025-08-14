namespace RowingApp.Common.Responses
{
    public class CatalogoResponse
    {
        public string catCodigo { get; set; }
        public string CodigoSap { get; set; }
        public string catCatalogo { get; set; }
        public int VerEnReclamosApp { get; set; }
        public int VerRequerimientosAPP { get; set; }
        public int VerRequerimientosEPP { get; set; }
        public int? VerEnSuministros { get; set; }
        public int? VerEnCalle { get; set; }
        public int? catDeshabilitado { get; set; }
        public string Modulo { get; set; }
        public decimal? Cantidad { get; set; }
    }
}