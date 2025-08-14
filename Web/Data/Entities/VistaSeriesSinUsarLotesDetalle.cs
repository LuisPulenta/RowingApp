using System.ComponentModel.DataAnnotations;

namespace RowingApp.Web.Data.Entities
{
    public class VistaSeriesSinUsarLotesDetalle
    {
        [Key]
        public int NROREGISTRO { get; set; }
        public int NROLOTECAB { get; set; }
        public string GRUPOH { get; set; }
        public string CAUSANTEH { get; set; }
        public string NROSERIESALIDA { get; set; }
        public string CODIGOSIAG { get; set; }
        public string CODIGOSAP { get; set; }
        public string Denominacion { get; set; }
    }
}