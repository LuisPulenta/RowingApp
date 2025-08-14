using System;
using System.ComponentModel.DataAnnotations;
namespace RowingApp.Web.Data.Entities
{
    public class LotesDetall
    {
        [Key]
        public int NROREGISTRO { get; set; }
        public int NROLOTECAB { get; set; }
        public string CODIGOSIAG { get; set; }
        public string CODIGOSAP { get; set; }
        public decimal CANTIDAD { get; set; }
        public string NROSERIESALIDA { get; set; }
        public string NROLOTEENTRADA { get; set; }
        public int TAG { get; set; }
        public int MARCACOINCIDE { get; set; }
        public int SerieUsada { get; set; }
        public DateTime? FechaUsada { get; set; }
        public int? IDInstalacionesEquipos { get; set; }
    }
}
