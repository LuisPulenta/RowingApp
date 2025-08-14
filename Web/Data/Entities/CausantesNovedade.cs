﻿using System;
using System.ComponentModel.DataAnnotations;

namespace RowingApp.Web.Data.Entities
{
    public class CausantesNovedade
    {
        [Key]
        public int IDNOVEDAD { get; set; }
        public string GRUPO { get; set; }
        public string CAUSANTE { get; set; }
        public DateTime FECHACARGA { get; set; }
        public DateTime FECHANOVEDAD { get; set; }
        public string EMPRESA { get; set; }
        public DateTime FECHAINICIO { get; set; }
        public DateTime FECHAFIN { get; set; }
        public string TIPONOVEDAD { get; set; }
        public string OBSERVACIONES { get; set; }
        public int VistaRRHH { get; set; }
        public int Idusuario { get; set; }

        public DateTime? FechaEstado { get; set; }
        public string ObservacionEstado { get; set; }
        public int? ConfirmaLeido { get; set; }
        public int? IDUsrEstado { get; set; }
        public string Estado { get; set; }

        public string LinkAdjunto1 { get; set; }
        public string ImageFullPath1 => string.IsNullOrEmpty(LinkAdjunto1)
        ? $"https://gaos2.keypress.com.ar/RowingAppApi/images/Novedades/noimage.png"
        : $"https://gaos2.keypress.com.ar/RowingAppApi{LinkAdjunto1.Substring(1)}";

        public string LinkAdjunto2 { get; set; }
        public string ImageFullPath2 => string.IsNullOrEmpty(LinkAdjunto2)
        ? $"https://gaos2.keypress.com.ar/RowingAppApi/images/Novedades/noimage.png"
        : $"https://gaos2.keypress.com.ar/RowingAppApi{LinkAdjunto1.Substring(1)}";
    }
}