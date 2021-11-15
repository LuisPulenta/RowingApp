using GenericApp.Common.Helpers;
using GenericApp.Common.Models;
using GenericApp.Common.Requests;
using GenericApp.Common.Responses;
using GenericApp.Common.Services;
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
    public class MedidoresPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private readonly IFilesHelper _filesHelper;

        private UsuarioAppResponse _usuarioLogueado;
        public UsuarioAppResponse UsuarioLogueado
        {
            get => _usuarioLogueado;
            set => SetProperty(ref _usuarioLogueado, value);
        }

        private bool _isRunning;
        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        private bool _isEnabled;
        public bool IsEnabled
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value);
        }

        private int _idPhoto;
        public int IdPhoto { get => _idPhoto; set => SetProperty(ref _idPhoto, value); }

        private int _idPhotoNro;
        public int IdPhotoNro { get => _idPhotoNro; set => SetProperty(ref _idPhotoNro, value); }

        private ObservableCollection<ObraDocumentoResponse> _images;
        public ObservableCollection<ObraDocumentoResponse> Images
        {
            get => _images;
            set => SetProperty(ref _images, value);
        }

        private ObservableCollection<ObraDocumentoResponse> _imagesTemp;
        public ObservableCollection<ObraDocumentoResponse> ImagesTemp
        {
            get => _imagesTemp;
            set => SetProperty(ref _imagesTemp, value);
        }

        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => SetProperty(ref _isRefreshing, value);
        }

        private MediaFile _file;
        public MediaFile File
        {
            get => _file;
            set => SetProperty(ref _file, value);
        }

        private ImageSource _imageSource;
        public ImageSource ImageSource
        {
            get => _imageSource;
            set => SetProperty(ref _imageSource, value);
        }



        private ObrasPosteResponse _obrasPoste;
        public ObrasPosteResponse ObrasPoste
        {
            get => _obrasPoste;
            set => SetProperty(ref _obrasPoste, value);
        }

        public List<ObraDocumentoResponse> MyObrasDocumentos { get; set; }

        private string _domicilio;
        public string Domicilio
        {
            get => _domicilio;
            set => SetProperty(ref _domicilio, value);
        }

        private string _ticket;
        public string Ticket
        {
            get => _ticket;
            set => SetProperty(ref _ticket, value);
        }

        private DelegateCommand _consultarCommand;
        public DelegateCommand ConsultarCommand => _consultarCommand ?? (_consultarCommand = new DelegateCommand(ConsultarAsync));


        public MedidoresPageViewModel(INavigationService navigationService, IApiService apiService, IFilesHelper filesHelper) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            _filesHelper = filesHelper;
            UsuarioLogueado = JsonConvert.DeserializeObject<UsuarioAppResponse>(Settings.UsuarioLogueado);
            IsEnabled = false;
            Title = "Medidores";
            instance = this;
            ImageSource = "noimage.png";
        }

        private async void ConsultarAsync()
        {
            if (string.IsNullOrEmpty(Ticket))
            {
                await App.Current.MainPage.DisplayAlert(
                    "Error",
                    "Debe ingresar un N° de Ticket",
                    "Aceptar");
                return;
            }

            IsRunning = true;
            IsEnabled = false;

            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                IsRunning = false;
                IsEnabled = false;
                await App.Current.MainPage.DisplayAlert(
                    "Error",
                    "Error de conexión",
                    "Aceptar");
                return;
            }

            string url = App.Current.Resources["UrlAPI"].ToString();


            var response = await _apiService.GetTicketAsync(url, "API", "/ObrasPostes/GetTicket", Ticket);

            if (!response.IsSuccess)
            {
                IsEnabled = false;
                IsRunning = false;
                await App.Current.MainPage.DisplayAlert("Error", "El N° de Ticket ingresado no existe.", "Aceptar");
                ObrasPoste = null;

                return;
            }

            Settings.ObrasPoste = JsonConvert.SerializeObject(response.Result);
            IsEnabled = true;
            IsRunning = false;

            ObrasPoste = response.Result;
            Domicilio = $"{ObrasPoste.DIRECCION} {ObrasPoste.NUMERACION} - {ObrasPoste.Localidad}";

            var response2 = await _apiService.GetObrasDocumentosAsync(url, "API", "/ObrasDocuments/GetObrasDocumentos/{0}", response.Result.NROREGISTRO);


            MyObrasDocumentos = (List<ObraDocumentoResponse>)response2.Result;

            var myListObrasDocumentos = MyObrasDocumentos.Select(a => new ObraDocumentoResponse()
            {
                FECHA=a.FECHA,
                NROOBRA = a.NROOBRA,
                NROREGISTROCAB = a.NROREGISTROCAB,
                OBSERVACION = a.OBSERVACION,
                DireccionFoto = a.DireccionFoto,
                LINK = a.LINK,
                TipoDeFoto = a.TipoDeFoto,
                Sector = a.Sector,
                NROREGISTRO = a.NROREGISTRO,
                NroLote = a.NroLote,
                MODULO = a.MODULO,
                Longitud = a.Longitud,
                Latitud = a.Latitud,
                GeneradoPor = a.GeneradoPor,
                Estante = a.Estante,
                FechaHsFoto = a.FechaHsFoto,
            });

            ImagesTemp = new ObservableCollection<ObraDocumentoResponse>(myListObrasDocumentos.OrderBy(o => o.TipoDeFoto));
            Images = new ObservableCollection<ObraDocumentoResponse>();
            IdPhoto = 0;
            if (Images.Count > 0)
            {
                IdPhoto = Images[0].NROREGISTRO;
            };
            foreach (ObraDocumentoResponse obraDocumentoResponse in ImagesTemp)
            {
                if (obraDocumentoResponse.TipoDeFoto >= 0 && obraDocumentoResponse.TipoDeFoto <= 3)
                {
                    Images.Add(obraDocumentoResponse);
                }
            }

        }

        #region Singleton

        private static MedidoresPageViewModel instance;
        public static MedidoresPageViewModel GetInstance()
        {
            return instance;
        }
        #endregion
    }
}