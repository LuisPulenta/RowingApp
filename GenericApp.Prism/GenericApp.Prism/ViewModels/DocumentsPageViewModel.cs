using GenericApp.Common.Helpers;
using GenericApp.Common.Models;
using GenericApp.Common.Requests;
using GenericApp.Common.Responses;
using GenericApp.Common.Services;
using GenericApp.Prism.ItemViewModels;
using Newtonsoft.Json;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace GenericApp.Prism.ViewModels
{
    public class DocumentsPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;

        private ObraResponse _obra;
        public ObraResponse Obra
        {
            get => _obra;
            set => SetProperty(ref _obra, value);
        }

        private ObservableCollection<DocumentoItemViewModel> _obraDocumentos;
        public ObservableCollection<DocumentoItemViewModel> ObraDocumentos
        {
            get => _obraDocumentos;
            set => SetProperty(ref _obraDocumentos, value);
        }

        private ObservableCollection<ObraDocumentoResponse> __obraDocumentosTemp;
        public ObservableCollection<ObraDocumentoResponse> ObraDocumentosTemp
        {
            get => __obraDocumentosTemp;
            set => SetProperty(ref __obraDocumentosTemp, value);
        }

        public List<DocumentoItemViewModel> MyObraDocumentos { get; set; }

        public DocumentsPageViewModel(INavigationService navigationService, IApiService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            Obra = JsonConvert.DeserializeObject<ObraResponse>(Settings.Obra);

        ObraDocumentos = new ObservableCollection<DocumentoItemViewModel>();
            LoadObraDocumentos();
            Title = "Documentos";
        }

        private void LoadObraDocumentos()
        {
            ObraDocumentosTemp = new ObservableCollection<ObraDocumentoResponse>(Obra.ObrasDocumentos);

            foreach (ObraDocumentoResponse obraDocumentoResponse in ObraDocumentosTemp)
            {
                if (obraDocumentoResponse.TipoDeFoto == 4)
                {
                    DocumentoItemViewModel obra = new DocumentoItemViewModel(_navigationService)
                    {
                        MODULO = obraDocumentoResponse.MODULO,
                        FECHA = obraDocumentoResponse.FECHA,
                        NROOBRA = obraDocumentoResponse.NROOBRA,
                        OBSERVACION = obraDocumentoResponse.OBSERVACION,
                        DireccionFoto = obraDocumentoResponse.DireccionFoto,
                        Estante = obraDocumentoResponse.Estante,
                        FechaHsFoto = obraDocumentoResponse.FechaHsFoto,
                        GeneradoPor = obraDocumentoResponse.GeneradoPor,
                        Latitud = obraDocumentoResponse.Latitud,
                        LINK = obraDocumentoResponse.LINK,
                        Longitud = obraDocumentoResponse.Longitud,
                        NroLote = obraDocumentoResponse.NroLote,
                        NROREGISTRO = obraDocumentoResponse.NROREGISTRO,
                        Sector = obraDocumentoResponse.Sector,
                        TipoDeFoto = obraDocumentoResponse.TipoDeFoto,
                    };

                    ObraDocumentos.Add(obra);
                }
            }

            //MyObraDocumentos = (List<ObraDocumentoResponse>)ObraDocumento.
        }
    }
}
