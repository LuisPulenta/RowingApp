﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GenericApp.Common.Requests
{
    public class AppInstalacionesEquipoRequest
    {
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
        public byte[] ImageArrayFIRMACLIENTE { get; set; }
        public string DocumentoFirmante { get; set; }
        public int? MismoFirmante { get; set; }
        public string TipoPedido { get; set; }
    }
}
