using System.Collections.Generic;
using System.Linq;

namespace GenericApp.Common.Responses
{
    public class ObraResponse
    {
        public int NroObra { get; set; }
        public string NombreObra { get; set; }
        public string ELEMPEP { get; set; }
        public string OBSERVACIONES { get; set; }
        public string Modulo { get; set; }
        public int HabilitaReclamosAPP { get; set; }
        public int? CORRESPONDEABONADOS { get; set; }
        public int CantObras { get; set; }
        public ICollection<ObraDocumentoResponse> ObrasDocumentos { get; set; }
        public int ObrasDocumentsNumber => ObrasDocumentos == null ? 0 : ObrasDocumentos.Count;
        public string ImageFullPath => ObrasDocumentos == null || ObrasDocumentos.Count == 0
            ? $"http://190.111.249.225/RowingAppApi/images/Obras/noimage.png"
            : ObrasDocumentos.FirstOrDefault().ImageFullPath;
    }
}