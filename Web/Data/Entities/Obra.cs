using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

namespace RowingApp.Web.Data.Entities
{
    public class Obra
    {
        private bool EsImagen(string link)
        {
            var ext = ObtenerExtension(link);
            return ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".gif";
        }

        private bool EsAudio(string link)
        {
            var ext = ObtenerExtension(link);
            return ext == ".mp3" || ext == ".wav" || ext == ".aac";
        }

        private bool EsVideo(string link)
        {
            var ext = ObtenerExtension(link);
            return ext == ".mp4" || ext == ".avi" || ext == ".mov";
        }

        private bool EsPdf(string link)
        {
            var ext = ObtenerExtension(link);
            return ext == ".pdf";
        }

        private string ObtenerExtension(string link)
        {
            if (string.IsNullOrEmpty(link))
                return string.Empty;

            return Path.GetExtension(link).ToLower();
        }

        [Key]
        public int NroObra { get; set; }

        public string NombreObra { get; set; }
        public string NroOE { get; set; }
        public string DefProy { get; set; }
        public string Central { get; set; }
        public string ELEMPEP { get; set; }
        public string OBSERVACIONES { get; set; }
        public int Finalizada { get; set; }
        public DateTime? FECHAFINALIZADA { get; set; }
        public int ULTIMAACTA { get; set; }
        public string SUPERVISORE { get; set; }
        public string CodigoEstado { get; set; }
        public string CodigoSubEstado { get; set; }
        public string Modulo { get; set; }
        public string GrupoAlmacen { get; set; }
        public string GrupoCausante { get; set; }
        public ICollection<ObrasDocumento> ObrasDocumentos { get; set; }
        public int HabilitaReclamosAPP { get; set; }
        public int? CORRESPONDEABONADOS { get; set; }
        public DateTime? FechaCierreElectrico { get; set; }
        public DateTime? FechaUltimoMovimiento { get; set; }
        public string POSX { get; set; }
        public string POSY { get; set; }
        public string Direccion { get; set; }
        public string TextoLocalizacion { get; set; }
        public string TextoClase { get; set; }
        public string TextoTipo { get; set; }
        public string TextoComponente { get; set; }
        public string CodigoDiametro { get; set; }
        public string Motivo { get; set; }
        public string Planos { get; set; }

        public int Photos => ObrasDocumentos == null ? 0 : ObrasDocumentos.Count(e => EsImagen(e.LINK));

        public int Audios => ObrasDocumentos == null ? 0 : ObrasDocumentos.Count(e => EsAudio(e.LINK));

        public int Videos => ObrasDocumentos == null ? 0 : ObrasDocumentos.Count(e => EsVideo(e.LINK));

        public int Pdfs => ObrasDocumentos == null ? 0 : ObrasDocumentos.Count(e => EsPdf(e.LINK));
    }
}