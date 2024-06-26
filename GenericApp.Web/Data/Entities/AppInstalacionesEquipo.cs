﻿using System;
using System.ComponentModel.DataAnnotations;
namespace GenericApp.Web.Data.Entities
{
    public class AppInstalacionesEquipo
    {
        [Key]
        public int IDRegistro { get; set; }
        public int NroObra { get; set; }
        public int IdUsuario { get; set; }
        public string Imei { get; set; }
        public DateTime Fecha { get; set; }
        public string Latitud { get; set; }
        public string Longitud { get; set; }
        public DateTime FechaInstalacion { get; set; }
        public string Grupo { get; set; }
        public string Causante { get; set; }
        public string Pedido { get; set; }
        public string NombreCliente { get; set; }
        public string ApellidoCliente { get; set; }
        public string Documento { get; set; }
        public string DomicilioInstalacion { get; set; }
        public string EntreCalles { get; set; }
        public string Firmacliente { get; set; }
        public string NombreApellidoFirmante { get; set; }
        public string TipoInstalacion { get; set; }
        public string EsAveria { get; set; }
        public int? Auditado { get; set; }
        public string DocumentoFirmante { get; set; }
        public int? MismoFirmante { get; set; }
        public string TipoPedido { get; set; }

        public string FirmaclienteImageFullPath => string.IsNullOrEmpty(Firmacliente)
     ? $"http://190.111.249.225/RowingAppApi/images/Instalaciones/noimage.png"
     : $"http://190.111.249.225/RowingAppApi{Firmacliente.Substring(1)}";
    }
}
