﻿using System.ComponentModel.DataAnnotations;
namespace RowingApp.Web.Data.Entities
{
    public class ObrasPosteCajaDetalleAPP
    {
        [Key]
        public int NROREGISTROD { get; set; }
        public int NROREGISTROCAB { get; set; }
        public string CATCODIGO { get; set; }
        public string CODIGOSAP { get; set; }
        public decimal CANTIDAD { get; set; }
    }
}
