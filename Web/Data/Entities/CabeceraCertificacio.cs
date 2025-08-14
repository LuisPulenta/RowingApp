using System;
using System.ComponentModel.DataAnnotations;

namespace RowingApp.Web.Data.Entities
{
    public class CabeceraCertificacio
    {
        [Key]
        public int ID { get; set; }
        public int NROOBRA { get; set; }
        public string DefProy { get; set; }
        public DateTime FECHACARGA { get; set; }
        public DateTime FECHADESPACHO { get; set; }
        public DateTime FECHAEJECUCION { get; set; }
        public string NombreObra { get; set; }
        public string NroOE { get; set; }
        public int? FINALIZADA { get; set; }
        public int? MATERIALESDESCONTADOS { get; set; }
        public string subCodigo { get; set; }
        public string CENTRAL { get; set; }
        public int? PREADICIONAL { get; set; }
        public int? NroPre { get; set; }
        public string SIPA { get; set; }
        public string OBSERVACION { get; set; }
        public string TIPIFICACION { get; set; }
        public int? FECHACORRESPONDENCIA { get; set; }
        public int? MARCADEVENTA { get; set; }
        public string NRO103 { get; set; }
        public string NRO105 { get; set; }
        public int? IDUSUARIOP { get; set; }
        public DateTime? FECHALIBERACION { get; set; }
        public int? IDUSUARIOL { get; set; }
        public int? NROORDENPAGO { get; set; }
        public decimal? VALORTOTAL { get; set; }
        public string PAGAR90 { get; set; }
        public decimal? VALOR90 { get; set; }
        public decimal? PRECIO90 { get; set; }
        public decimal? MONTO90 { get; set; }
        public string PAGAR10 { get; set; }
        public decimal? VALOR10 { get; set; }
        public decimal? PRECIO10 { get; set; }
        public decimal? MONTO10 { get; set; }
        public int? IDUSUARIOFR { get; set; }
        public DateTime? FECHAFONDOREPARO { get; set; }
        public int? NROPAGOFR { get; set; }
        public string CODIGOPRODUCCION { get; set; }
        public string ObservacionO { get; set; }
        public string Clase { get; set; }
        public decimal? VALORTOTALC { get; set; }
        public decimal? VALORTOTALT { get; set; }
        public decimal? PorcAplicado { get; set; }
        public string PAGARX { get; set; }
        public decimal? VALORX { get; set; }
        public decimal? PRECIO10X { get; set; }
        public decimal MONTOX { get; set; }
        public string CodCausanteC { get; set; }
        public int? Cobrar { get; set; }
        public string Presentado { get; set; }
        public string Estado { get; set; }
        public string Modulo { get; set; }
        public int? IdUsuario { get; set; }
        public string Terminal { get; set; }
        public DateTime? Fecha103 { get; set; }
        public DateTime? Fecha105 { get; set; }
        public int? MesImputacion { get; set; }
        public string Objeto { get; set; }
        public decimal? PorcActa { get; set; }
    }
}
