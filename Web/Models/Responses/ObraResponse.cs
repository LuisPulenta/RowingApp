using System;
using System.Collections.Generic;
using System.Linq;

namespace RowingApp.Common.Responses
{
    public class ObraResponse
    {
        public int NroObra { get; set; }
        public string NombreObra { get; set; }
        public string NroOE { get; set; }
        public string DefProy { get; set; }
        public string Central { get; set; }
        public string ELEMPEP { get; set; }
        public string OBSERVACIONES { get; set; }
        public int Finalizada { get; set; }
        public DateTime? FECHAFINALIZADA { get; set; }
        public string Modulo { get; set; }
        public string GrupoAlmacen { get; set; }
        public string GrupoCausante { get; set; }
        public int HabilitaReclamosAPP { get; set; }
        public int? CORRESPONDEABONADOS { get; set; }
        public DateTime? FechaCierreElectrico { get; set; }
        public int CantObras { get; set; }
        public ICollection<ObraDocumentoResponse> ObrasDocumentos { get; set; }
        public int ObrasDocumentsNumber => ObrasDocumentos == null ? 0 : ObrasDocumentos.Count;
        public string ImageFullPath => ObrasDocumentos == null || ObrasDocumentos.Count == 0
            ? $"http://190.111.249.225/RowingAppApi/images/Obras/noimage.png"
            : ObrasDocumentos.FirstOrDefault().ImageFullPath;
    }
}