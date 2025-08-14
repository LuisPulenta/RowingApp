using System;
using System.ComponentModel.DataAnnotations;

namespace RowingApp.Web.Data.Entities
{
    public class Causante
    {
        [Key]
        public int NroCausante { get; set; }
        public string codigo { get; set; }
        public string nombre { get; set; }
        public string encargado { get; set; }
        public string telefono { get; set; }
        public string grupo { get; set; }
        public string NroSAP { get; set; }
        public bool estado { get; set; }
        public string RazonSocial { get; set; }
        public string LinkFoto { get; set; }
        public string direccion { get; set; }
        public int Numero { get; set; }
        public string TelefonoContacto1 { get; set; }
        public string TelefonoContacto2 { get; set; }
        public string TelefonoContacto3 { get; set; }
        public DateTime? fecha { get; set; }
        public string NotasCausantes { get; set; }
        public string ciudad { get; set; }
        public string Provincia { get; set; }
        public int CodigoSupervisorObras { get; set; }
        public string ZonaTrabajo { get; set; }
        public string NombreActividad { get; set; }
        public string notas { get; set; }
        public string PerteneceCuadrilla { get; set; }
        public int? HabilitaInstalacionesAPP { get; set; }
        public int? VisualizaSPR { get; set; }
        public string FirmaDigitalAPP { get; set; }
        

        public string ImageFullPath => string.IsNullOrEmpty(LinkFoto)
        ? $"http://190.111.249.225/RowingAppApi/images/Causantes/nouser.png"
        : $"http://190.111.249.225/RowingAppApi{LinkFoto.Substring(1)}";

        public string FirmaFullPath => string.IsNullOrEmpty(FirmaDigitalAPP)
        ? $"http://190.111.249.225/RowingAppApi/images/Recibos/noimage.png"
        : $"http://190.111.249.225/RowingAppApi{FirmaDigitalAPP.Substring(1)}";
    }
}
