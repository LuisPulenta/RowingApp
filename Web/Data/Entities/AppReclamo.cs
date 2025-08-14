using System;
using System.ComponentModel.DataAnnotations;

namespace RowingApp.Web.Data.Entities
{
    public class AppReclamo
    {
        [Key]
        public int IDReclamo { get; set; }
        public DateTime FechaCarga { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string DNI { get; set; }
        public int NombrePropio { get; set; }
        public string NombreRepresentante { get; set; }
        public string ApellidoRepresentante { get; set; }
        public string DNIRepresentante { get; set; }
        public string Direccion { get; set; }
        public string Localidad { get; set; }
        public string CodPostal { get; set; }
        public string Nis { get; set; }
        public string NroCuenta { get; set; }
        public string CoincideDireccion { get; set; }
        public string DireccionContacto { get; set; }
        public string LocalidadContacto { get; set; }
        public string CodPostalContacto { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public int? ErroresEnFacturacion { get; set; }
        public int? ResarcimientoPorDanios { get; set; }
        public int? SuspensionDeSuministro { get; set; }
        public int? MalaAtencionComercial { get; set; }
        public int? NegativaDeConexion { get; set; }
        public int? InconvenienteDeTension { get; set; }
        public int? FacturaFueraDeTerminoNoRecibidas { get; set; }
        public string Reclamo { get; set; }
        public string Foto1 { get; set; }
        public string Foto2 { get; set; }
        public string Foto3 { get; set; }
        public string Pdf1 { get; set; }
        public string Pdf2 { get; set; }
        public string Pdf3 { get; set; }
    }
}

