﻿using System;
using System.ComponentModel.DataAnnotations;

namespace GenericApp.Web.Data.Entities
{
    public class ObrasReparo
    {
        [Key]
        public int NROREGISTRO { get; set; }
        public int NROOBRA { get; set; }
        public DateTime? FECHAALTA { get; set; }
        public DateTime? FECHAINICIO { get; set; }
        public DateTime? FECHACUMPLIMENTO { get; set; }
        public string REQUERIDOPOR { get; set; }
        public string SUBCONTRATISTA { get; set; }
        public string SUBCONTRATISTAREPARO { get; set; }
        public string CODCAUSANTE { get; set; }
        public string NROCTOC { get; set; }
        public string DIRECCION { get; set; }
        public string ALTURA { get; set; }
        public string LATITUD { get; set; }
        public string LONGITUD { get; set; }
        public int? CODTIPOSTDRPARO { get; set; }
        public string ESTADOSUBCON { get; set; }
        public string RECURSOS { get; set; }
        public double? MONTODISPONIBLE { get; set; }
        public string GRUA { get; set; }
        public int? IDUsuario { get; set; }
        public string Terminal { get; set; }
        public string Observaciones { get; set; }
        public string Foto1 { get; set; }
        public string TipoVereda { get; set; }
        public int? CantidadMTL { get; set; }
        public int? Ancho { get; set; }
        public int? Profundidad { get; set; }
        public DateTime? FechaCierreElectrico { get; set; }

        public string ImageFullPath => string.IsNullOrEmpty(Foto1)
        ? $"http://190.111.249.225/RowingAppApi/images/ObrasReparos/noimage.png"
        : $"http://190.111.249.225/RowingAppApi{Foto1.Substring(1)}";
    }
}