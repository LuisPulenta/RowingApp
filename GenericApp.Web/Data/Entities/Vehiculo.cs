using System;
using System.ComponentModel.DataAnnotations;

namespace GenericApp.Web.Data.Entities
{
    public class Vehiculo
    {
        [Key]
        public Int16 CODVEH { get; set; }
        public string NUMCHA { get; set; }
        public string CodProducto { get; set; }
        public Int16? ANIOFA { get; set; }
        public string Descripcion { get; set; }
        public string NMOTOR { get; set; }
        public string CHASIS { get; set; }
        public int? FechaVencITV { get; set; }
        public string NroPolizaSeguro { get; set; }
        public string CentroCosto { get; set; }
        public string PropiedadDe { get; set; }
        public string Telepase { get; set; }
        public int? KMHSACTUAL { get; set; }
        public Byte? UsaHoras { get; set; }
        public Byte? Habilitado { get; set; }
        public int? FechaVencObleaGAS { get; set; }
        public string Modulo { get; set; }
        public string CAMPOMEMO { get; set; }
       
    }
}
