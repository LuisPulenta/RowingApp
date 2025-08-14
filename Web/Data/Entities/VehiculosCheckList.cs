using System;
using System.ComponentModel.DataAnnotations;

namespace RowingApp.Web.Data.Entities
{
    public class VehiculosCheckList
    {
        [Key]
        public int IDCheckList { get; set; }
        public DateTime Fecha { get; set; }
        public int IDUser { get; set; }
        public int IdCliente { get; set; }
        public int IDVehiculo { get; set; }
        public string VTV { get; set; }
        public DateTime? FechaVencVTV { get; set; }
        public string VTH { get; set; }
        public DateTime? FechaVencVTH { get; set; }
        public string Cubiertas { get; set; }
        public string CorreaCinturon { get; set; }
        public string ApoyaCabezas { get; set; }
        public string Limpiavidrios { get; set; }
        public string Espejos { get; set; }
        public string IndicadoresDeGiro { get; set; }
        public string Bocina { get; set; }
        public string DispositivoPAT { get; set; }
        public string Ganchos { get; set; }
        public string AlarmaRetroceso { get; set; }
        public string ManguerasCircuitoHidraulico { get; set; }
        public string FarosDelanteros { get; set; }
        public string FarosTraseros { get; set; }
        public string LuzPosicion { get; set; }
        public string LuzFreno { get; set; }
        public string LuzRetroceso { get; set; }
        public string LuzEmergencia { get; set; }
        public string BalizaPortatil { get; set; }
        public string Matafuegos { get; set; }
        public string IdentificadorEmpresa { get; set; }
        public string SobreSalientesPeligro { get; set; }
        public string DiagramaDeCarga { get; set; }
        public string Fajas { get; set; }
        public string Grilletes { get; set; }
        public string CintaSujecionCarga { get; set; }
        public string JefeDirecto { get; set; }
        public string ResponsableVehiculo { get; set; }
        public string Observaciones { get; set; }
        public string GrupoC { get; set; }
        public string CausanteC { get; set; }
        public string DNI { get; set; }
        public string ApellidoNombre { get; set; }
        public string Seguro { get; set; }
        public DateTime? FechaVencSeguro { get; set; }

    }
}
