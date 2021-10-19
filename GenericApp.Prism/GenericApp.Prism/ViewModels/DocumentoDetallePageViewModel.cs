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
    public class DocumentoDetallePageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;

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
            var a = 1;
        }
    }
}
