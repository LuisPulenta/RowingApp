using System;
using System.ComponentModel.DataAnnotations;

namespace GenericApp.Web.Data.Entities
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
        public double Velocidad { get; set; }
        public int Bateria { get; set; }
        public DateTime Fecha { get; set; }
        public string Modulo { get; set; }
    }
}
