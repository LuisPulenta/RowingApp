using System;
using System.ComponentModel.DataAnnotations;

namespace RowingApp.Web.Data.Entities
{
    public class UsuariosGeo
    {
        [Key]
        public int IDGEO { get; set; }
        public int IdUsuario { get; set; }
        public string UsuarioStr { get; set; }
        public string LATITUD { get; set; }
        public string LONGITUD { get; set; }
        public string PIN { get; set; }
        public string PosicionCalle { get; set; }
        public decimal Velocidad { get; set; }
        public decimal Bateria { get; set; }
        public DateTime Fecha { get; set; }
        public string Modulo { get; set; }
        public int Origen { get; set; }
    }
}
