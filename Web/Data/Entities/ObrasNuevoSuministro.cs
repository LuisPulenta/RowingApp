using System;
using System.ComponentModel.DataAnnotations;

namespace RowingApp.Web.Data.Entities
{
    public class ObrasNuevoSuministro
    {
        [Key]
        public int NROSUMINISTRO { get; set; }
        public int NROOBRA { get; set; }
        public DateTime? FECHA { get; set; }
        public string APELLIDONOMBRE { get; set; }
        public string DNI { get; set; }
        public string TELEFONO { get; set; }
        public string EMAIL { get; set; }
        public string CUADRILLA { get; set; }
        public string GRUPOC { get; set; }
        public string CAUSANTEC { get; set; }
        public string DIRECTA { get; set; }
        public string DOMICILIO { get; set; }
        public string BARRIO { get; set; }
        public string LOCALIDAD { get; set; }
        public string PARTIDO { get; set; }
        public string ANTESFOTO1 { get; set; }
        public string ANTESFOTO2 { get; set; }
        public string DESPUESFOTO1 { get; set; }
        public string DESPUESFOTO2 { get; set; }
        public string FOTODNIFRENTE { get; set; }
        public string FOTODNIREVERSO { get; set; }
        public string FIRMACLIENTE { get; set; }
        public string ENTRECALLES1 { get; set; }
        public string ENTRECALLES2 { get; set; }
        public string MEDIDORCOLOCADO { get; set; }
        public string MEDIDORVECINO { get; set; }
        public string TIPORED { get; set; }
        public string CORTE { get; set; }
        public string DENUNCIA { get; set; }
        public string ENRE { get; set; }
        public string OTRO { get; set; }
        public string CONEXIONDIRECTA { get; set; }
        public string RETIROCONEXION { get; set; }
        public string RETIROCRUCECALLE { get; set; }
        public int? MTSCABLERETIRADO { get; set; }
        public string TRABAJOCONHIDRO { get; set; }
        public string POSTEPODRIDO { get; set; }
        public string OBSERVACIONES { get; set; }
        public int? POTENCIACONTRATADA { get; set; }
        public int? TENSIONCONTRATADA { get; set; }
        public int? KITNRO { get; set; }
        public int? IDCERTIFMATERIALES { get; set; }
        public int? IDCERTIFBAREMO { get; set; }
        public string PosX { get; set; }
        public string PosY { get; set; }
        public int? IDUserCarga { get; set; }
        public string ANTESFOTO1ImageFullPath => string.IsNullOrEmpty(ANTESFOTO1)
       ? $"https://gaos2.keypress.com.ar/RowingAppApi/images/ObrasSuministros/noimage.png"
       : $"https://gaos2.keypress.com.ar/RowingAppApi{ANTESFOTO1.Substring(1)}";

        public string ANTESFOTO2ImageFullPath => string.IsNullOrEmpty(ANTESFOTO2)
       ? $"https://gaos2.keypress.com.ar/RowingAppApi/images/ObrasSuministros/noimage.png"
       : $"https://gaos2.keypress.com.ar/RowingAppApi{ANTESFOTO2.Substring(1)}";

        public string DESPUESFOTO1ImageFullPath => string.IsNullOrEmpty(DESPUESFOTO1)
       ? $"https://gaos2.keypress.com.ar/RowingAppApi/images/ObrasSuministros/noimage.png"
       : $"https://gaos2.keypress.com.ar/RowingAppApi{DESPUESFOTO1.Substring(1)}";

        public string DESPUESFOTO2ImageFullPath => string.IsNullOrEmpty(DESPUESFOTO2)
       ? $"https://gaos2.keypress.com.ar/RowingAppApi/images/ObrasSuministros/noimage.png"
       : $"https://gaos2.keypress.com.ar/RowingAppApi{DESPUESFOTO2.Substring(1)}";

        public string FOTODNIFRENTEImageFullPath => string.IsNullOrEmpty(FOTODNIFRENTE)
       ? $"https://gaos2.keypress.com.ar/RowingAppApi/images/ObrasSuministros/noimage.png"
       : $"https://gaos2.keypress.com.ar/RowingAppApi{FOTODNIFRENTE.Substring(1)}";

        public string FOTODNIREVERSOImageFullPath => string.IsNullOrEmpty(FOTODNIREVERSO)
       ? $"https://gaos2.keypress.com.ar/RowingAppApi/images/ObrasSuministros/noimage.png"
       : $"https://gaos2.keypress.com.ar/RowingAppApi{FOTODNIREVERSO.Substring(1)}";

        public string FIRMACLIENTEImageFullPath => string.IsNullOrEmpty(FIRMACLIENTE)
      ? $"https://gaos2.keypress.com.ar/RowingAppApi/images/ObrasSuministros/noimage.png"
      : $"https://gaos2.keypress.com.ar/RowingAppApi{FIRMACLIENTE.Substring(1)}";
    }
}
