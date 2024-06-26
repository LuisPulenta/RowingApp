﻿using System;
using System.ComponentModel.DataAnnotations;
namespace GenericApp.Web.Data.Entities
{
    public class CausantesJuiciosNotificacione
    {
        [Key]
        public int IDNOTIFICACION    { get; set; }
        public int IDJUICIO { get; set; }
        public DateTime? FECHACARGA { get; set; }
        public string TIPO { get; set; }
        public string TITULO { get; set; }
        public string OBSERVACIONES { get; set; }
        public string MONEDA { get; set; }
        public decimal? MONTO { get; set; }
        public string TIPOTRANSACCION { get; set; }
        public string CONDICIONPAGO { get; set; }
        public string NROFACTURA { get; set; }
        public string LUGAR { get; set; }
        public string PARTICIPANTES { get; set; }
        public DateTime? FECHAECHO { get; set; }
        public DateTime? FECHAVENCIMIENTO { get; set; }
        public string LINKARCHIVO { get; set; }
        public string LINKARCHIVOFullPath => string.IsNullOrEmpty(LINKARCHIVO)
       ? $"http://190.111.249.225/RowingAppApi/images/Legales/noimage.png"
       : $"http://190.111.249.225/RowingAppApi{LINKARCHIVO.Substring(1)}";
    }
}
