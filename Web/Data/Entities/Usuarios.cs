using System.ComponentModel.DataAnnotations;

namespace RowingApp.Web.Data.Entities
{
    public class Usuario
    {
        [Key]
        public int IDUsuario { get; set; }
        public string Login { get; set; }
        public string CodigoCausante { get; set; }
        public string Contrasena { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int? AutorWOM { get; set; }
        public byte Estado { get; set; }
        public int? HabilitaAPP { get; set; }
        public int? HabilitaFotos { get; set; }
        public int? HabilitaReclamos { get; set; }
        public int? HabilitaSSHH { get; set; }
        public int? HabilitaRRHH { get; set; }
        public int? HabilitaMedidores { get; set; }
        public string HabilitaFlotas { get; set; }
        public int? ReHabilitaUsuarios { get; set; }
        public string Modulo { get; set; }
        public string CODIGOGRUPO { get; set; }
        public int? FechaCaduca { get; set; }
        public int? IntentosInvDiario { get; set; }
        public int? OpeAutorizo { get; set; }
        public int? HabilitaNuevoSuministro { get; set; }
        public int? HabilitaVeredas { get; set; }
        public int? HabilitaJuicios { get; set; }
        public int? HabilitaPresentismo { get; set; }
        public int? HabilitaSeguimientoUsuarios { get; set; }
        public int? HabilitaVerObrasCerradas { get; set; }
        public int? HabilitaElementosCalle { get; set; }
        public int? HabilitaCertificacion { get; set; }
        public byte? CONCEPTOMOVA { get; set; }
        public int? LimitarGrupo { get; set; }
        public byte? RUBRO { get; set; }
        public string FirmaUsuario { get; set; }
        public byte? CONCEPTOMOV { get; set; }
        public string AppIMEI { get; set; }
        
        public string FirmaUsuarioImageFullPath => string.IsNullOrEmpty(FirmaUsuario)
      ? $"https://gaos2.keypress.com.ar/RowingAppApi/images/ObrasSuministros/noimage.png"
      : $"https://gaos2.keypress.com.ar/RowingAppApi{FirmaUsuario.Substring(1)}";
    }
}
