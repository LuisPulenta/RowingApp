using System;
using System.ComponentModel.DataAnnotations;

namespace RowingApp.Web.Data.Entities

{
    public class VistaAppInstalacionesEquipo
    {
        [Key]
        public int IDRegistro { get; set; }

        [Display(Name = "N° Obra")]
        public int NroObra { get; set; }

        [Display(Name = "Id Usuario")]
        public int IdUsuario { get; set; }

        public string Imei { get; set; }

        public DateTime Fecha { get; set; }

        public string Latitud { get; set; }

        public string Longitud { get; set; }

        [Display(Name = "Fecha Instalación")]
        public DateTime FechaInstalacion { get; set; }

        public string Grupo { get; set; }

        public string Causante { get; set; }

        public string Pedido { get; set; }

        [Display(Name = "Nombre Cliente")]
        public string NombreCliente { get; set; }

        [Display(Name = "Apellido Cliente")]
        public string ApellidoCliente { get; set; }

        public string Documento { get; set; }

        [Display(Name = "Domicilio Instalación")]
        public string DomicilioInstalacion { get; set; }

        [Display(Name = "Entre Calles")]
        public string EntreCalles { get; set; }

        [Display(Name = "Firma Cliente")]
        public string Firmacliente { get; set; }

        [Display(Name = "Nombre Apellido Cliente")]
        public string NombreApellidoFirmante { get; set; }

        [Display(Name = "Tipo Instalación")]
        public string TipoInstalacion { get; set; }

        [Display(Name = "Es Avería")]
        public string EsAveria { get; set; }

        public int Auditado { get; set; }

        public string DocumentoFirmante { get; set; }
        public int? MismoFirmante { get; set; }
        public string TipoPedido { get; set; }
        public string NombreCausante { get; set; }

        public string FirmaclienteImageFullPath => string.IsNullOrEmpty(Firmacliente)
     ? $"https://gaos2.keypress.com.ar/RowingAppApi/images/Instalaciones/noimage.png"
     : $"https://gaos2.keypress.com.ar/RowingAppApi{Firmacliente.Substring(1)}";

        public string NombreCompleto => $"{ApellidoCliente} {NombreCliente}";
        public string PedidoCompleto => $"{TipoPedido}{Pedido}";
    }
}
