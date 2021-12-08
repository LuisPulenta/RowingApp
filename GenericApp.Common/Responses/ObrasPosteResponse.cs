using System.Collections.Generic;
using System.Linq;

namespace GenericApp.Common.Responses
{
    public class ObrasPosteResponse
    {
        public int NROREGISTRO { get; set; }
        public int NROOBRA { get; set; }
        public string ASTICKET { get; set; }
        public string Cliente { get; set; }
        public string DIRECCION { get; set; }
        public string NUMERACION { get; set; }
        public string Localidad { get; set; }
        public string Telefono { get; set; }
        public string TipoImput { get; set; }
        public string CERTIFICADO { get; set; }

        public string SerieMedidorColocado { get; set; }
        public string Precinto { get; set; }
        public string CajaDAE { get; set; }
        public string OBSERVACIONES { get; set; }
        public string Lindero1 { get; set; }
        public string Lindero2 { get; set; }

        public int CantObras { get; set; }
        //public ICollection<ObraDocumentoResponse> ObrasDocumentos { get; set; }
        //public int ObrasDocumentsNumber => ObrasDocumentos == null ? 0 : ObrasDocumentos.Count;
        //public string ImageFullPath => ObrasDocumentos == null || ObrasDocumentos.Count == 0 $"http://keypress.serveftp.net:88/RowingAppApi2/images/Obras/noimage.png": ObrasDocumentos.FirstOrDefault().ImageFullPath;
    }
}
