using GenericApp.Common.Helpers;
using GenericApp.Common.Responses;
using GenericApp.Common.Services;
using Newtonsoft.Json;
using Prism.Navigation;
using Syncfusion.Pdf.Parsing;
using System.IO;
using System.Reflection;
using Xamarin.Forms.Internals;

namespace GenericApp.Prism.ViewModels
{
    [Preserve(AllMembers = true)]
    public class DocumentoDetallePageViewModel : ViewModelBase
    {
        public bool IsPdf { get; set; }
        public string Uri { get; set; }

        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;

        private Stream m_pdfDocumentStream;
        public Stream PdfDocumentStream
        {
            get => m_pdfDocumentStream;
            set => SetProperty(ref m_pdfDocumentStream, value);
        }

        private ObraDocumentoResponse _obraDocumento;
        public ObraDocumentoResponse ObraDocumento
        {
            get => _obraDocumento;
            set => SetProperty(ref _obraDocumento, value);
        }

        private string _pDFFile;
        public string PDFFile
        {
            get => _pDFFile;
            set => SetProperty(ref _pDFFile, value);
        }

        #region Singleton

        private static DocumentoDetallePageViewModel instance;
        public static DocumentoDetallePageViewModel GetInstance()
        {
            return instance;
        }
        #endregion

        public DocumentoDetallePageViewModel(INavigationService navigationService, IApiService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            instance = this;
            ObraDocumento = JsonConvert.DeserializeObject<ObraDocumentoResponse>(Settings.Documento);
            PDFFile = ObraDocumento.LINK;
            Title = ObraDocumento.OBSERVACION;
            PdfDocumentStream = typeof(App).GetTypeInfo().Assembly.GetManifestResourceStream("GenericApp.Prism.Assets2.xaml.pdf");
            Uri = PDFFile;
            IsPdf = true;
        }
    }
}
