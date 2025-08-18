using System;

namespace Web.Models.Request
{
    public class CabeceraCertificacioRequest
    {
        public int ID { get; set; }
        public int NROOBRA { get; set; }
        public string DefProy { get; set; }
        public DateTime FECHACARGA { get; set; }
        public DateTime FECHADESPACHO { get; set; }
        public DateTime FECHAEJECUCION { get; set; }
        public string NombreObra { get; set; }
        public string NroOE { get; set; }        
        public string subCodigo { get; set; }
        public string CENTRAL { get; set; }        
        public int? NroPre { get; set; }        
        public string OBSERVACION { get; set; }        
        public int? FECHACORRESPONDENCIA { get; set; }
        public DateTime? FECHALIBERACION { get; set; }        
        public decimal? VALORTOTAL { get; set; }        
        public decimal? VALOR90 { get; set; }
        public decimal? PRECIO90 { get; set; }
        public decimal? MONTO90 { get; set; }        
        public string CODIGOPRODUCCION { get; set; }        
        public decimal? VALORTOTALC { get; set; }
        public decimal? VALORTOTALT { get; set; }
        public string CodCausanteC { get; set; }
        public string Modulo { get; set; }
        public int? IdUsuario { get; set; }
        public string Terminal { get; set; }       
        public int? MesImputacion { get; set; }
        public string Objeto { get; set; }
        public decimal? PorcActa { get; set; }
    }
}
