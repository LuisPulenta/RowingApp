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
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace GenericApp.Prism.ViewModels
{
    public class TakePhoto2PageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IGeolocatorService _geolocatorService;
        private readonly IApiService _apiService;
        private readonly IFilesHelper _filesHelper;

        private MediaFile _file;
        public MediaFile File
        {
            get => _file;
            set => SetProperty(ref _file, value);
        }

        private ObservableCollection<ObraDocumentoResponse> _images;
        public ObservableCollection<ObraDocumentoResponse> Images
        {
            get => _images;
            set => SetProperty(ref _images, value);
        }

        private string _nroObra;
        public string NroObra
        {
            get => _nroObra;
            set => SetProperty(ref _nroObra, value);
        }

        private Position _position;

        private string _nombreObra;
        public string NombreObra
        {
            get => _nombreObra;
            set => SetProperty(ref _nombreObra, value);
        }

        private string _remarks;
        public string Remarks
        {
            get => _remarks;
            set => SetProperty(ref _remarks, value);
        }

        private TipoFoto _tFoto;
        public TipoFoto TFoto
        {
            get => _tFoto;
            set => SetProperty(ref _tFoto, value);
        }

        private float? _latitud;
        public float? Latitud
        {
            get => _latitud;
            set => SetProperty(ref _latitud, value);
        }

        private float? _longitud;
        public float? Longitud
        {
            get => _longitud;
            set => SetProperty(ref _longitud, value);
        }

        private string _direccionFoto;
        public string DireccionFoto
        {
            get => _direccionFoto;
            set => SetProperty(ref _direccionFoto, value);
        }

        private int _tipoDeFoto;
        public int TipoDeFoto
        {
            get => _tipoDeFoto;
            set => SetProperty(ref _tipoDeFoto, value);
        }


        private ObservableCollection<TipoFoto> _tiposFoto;
        public ObservableCollection<TipoFoto> TiposFoto
        {
            get => _tiposFoto;
            set => SetProperty(ref _tiposFoto, value);
        }

        private string _eLEMPEP;
        public string ELEMPEP
        {
            get => _eLEMPEP;
            set => SetProperty(ref _eLEMPEP, value);
        }

        private bool _isRunning;
        private bool _isEnabled;
        private string _sourcePage;

        private ImageSource _imageSource;


        private string __remark;
        public string Remark
        {
            get => __remark;
            set => SetProperty(ref __remark, value);
        }

        private ObraResponse _obra;
        public ObraResponse Obra
        {
            get => _obra;
            set => SetProperty(ref _obra, value);
        }

        private ObrasPosteResponse _obrasPoste;
        public ObrasPosteResponse ObrasPoste
        {
            get => _obrasPoste;
            set => SetProperty(ref _obrasPoste, value);
        }

        public string SourcePage
        {
            get => _sourcePage;
            set => SetProperty(ref _sourcePage, value);
        }

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public bool IsEnabled
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value);
        }

        public ImageSource ImageSource
        {
            get => _imageSource;
            set => SetProperty(ref _imageSource, value);
        }

        private DelegateCommand _cancelCommand;
        private DelegateCommand _saveCommand;
        private DelegateCommand _takePhotoCommand;

        public DelegateCommand CancelCommand => _cancelCommand ?? (_cancelCommand = new DelegateCommand(Cancel));
        public DelegateCommand SaveCommand => _saveCommand ?? (_saveCommand = new DelegateCommand(Save));
        public DelegateCommand TakePhotoCommand => _takePhotoCommand ?? (_takePhotoCommand = new DelegateCommand(TakePhoto));

        public IFilesHelper FilesHelper { get; }

        public TakePhoto2PageViewModel(INavigationService navigationService, IApiService apiService, IFilesHelper filesHelper, IGeolocatorService geolocatorService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            _geolocatorService = geolocatorService;
            _filesHelper = filesHelper;
            FilesHelper = filesHelper;
            Title = "Tomar Fotografía";
            IsEnabled = true;
            ImageSource = "noimage";
            LoadTiposFoto();
            Obra = JsonConvert.DeserializeObject<ObraResponse>(Settings.Obra);
            ObrasPoste = JsonConvert.DeserializeObject<ObrasPosteResponse>(Settings.ObrasPoste);
        }

        private void LoadTiposFoto()
        {
            TiposFoto = new ObservableCollection<TipoFoto>();
            TiposFoto.Add(new TipoFoto { Codigo = 4, Descripcion = "N° de Medidor Colocado", });
            TiposFoto.Add(new TipoFoto { Codigo = 5, Descripcion = "Estado de medidor retirado", });
            TiposFoto.Add(new TipoFoto { Codigo = 6, Descripcion = "N° de precinto", });
            TiposFoto.Add(new TipoFoto { Codigo = 7, Descripcion = "N° de tapa o caja", });
            TiposFoto.Add(new TipoFoto { Codigo = 8, Descripcion = "Lindero 1", });
            TiposFoto.Add(new TipoFoto { Codigo = 9, Descripcion = "Lindero 2", });
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            if (parameters.ContainsKey("obra"))
            {
                Obra = parameters.GetValue<ObraResponse>("obra");
                Title = Obra.NombreObra;
                Images = new ObservableCollection<ObraDocumentoResponse>(Obra.ObrasDocumentos);
                NroObra = Obra.NroObra.ToString();
                NombreObra = Obra.NombreObra;
                ELEMPEP = Obra.ELEMPEP;
            }

            SourcePage = parameters.GetValue<string>("sourcePage");
        }

        private async void Cancel()
        {
            await _navigationService.GoBackAsync();
        }

        private async void TakePhoto()
        {
            await CrossMedia.Current.Initialize();

            string source = await Application.Current.MainPage.DisplayActionSheet(
              "De donde quiere tomar la foto?",
              "Cancelar",
              null,
              "Galería",
              "Cámara");

            if (source == "Cancelar")
            {
                _file = null;
                return;
            }

            if (source == "Cámara")
            {
                if (!CrossMedia.Current.IsCameraAvailable)
                {
                    await App.Current.MainPage.DisplayAlert("Error", "La cámara no está disponible", "Aceptar");
                    return;
                }

                _file = await CrossMedia.Current.TakePhotoAsync(
                new StoreCameraMediaOptions
                {
                    Directory = "Sample",
                    Name = "test.jpg",
                    PhotoSize = PhotoSize.Small,
                }
            );
            }
            else
            {
                if (!CrossMedia.Current.IsPickPhotoSupported)
                {
                    await App.Current.MainPage.DisplayAlert("Error", "La Galería no está disponible", "Aceptar");
                    return;
                }

                _file = await CrossMedia.Current.PickPhotoAsync();
            }

            if (_file != null)
            {
                ImageSource = ImageSource.FromStream(() =>
                {
                    var stream = _file.GetStream();
                    return stream;
                });
            }
            IsRunning = false;
        }

        private async void Save()
        {
            if (TFoto == null)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Debe seleccionar el Tipo de Foto.", "Aceptar");
                return;
            }

            if (_file == null)
            {
                await App.Current.MainPage.DisplayAlert("Error", "No ha sacado foto", "Aceptar");
                return;
            }
            else
            {
                ImageSource = ImageSource.FromStream(() =>
                {
                    var stream = _file.GetStream();
                    return stream;
                });
                File = _file;
            }

            await _geolocatorService.GetLocationAsync();

            if (_geolocatorService.Latitude == 0 && _geolocatorService.Longitude == 0)
            {
                IsEnabled = true;
                await App.Current.MainPage.DisplayAlert(
                    "Error",
                    "Error de Geolocalización.",
                    "Aceptar");
                //await _navigationService.GoBackAsync();
                return;
            }

            _position = new Position(_geolocatorService.Latitude, _geolocatorService.Longitude);
            Latitud = (float)_geolocatorService.Latitude;
            Longitud = (float)_geolocatorService.Longitude;
            Geocoder geoCoder = new Geocoder();
            IEnumerable<string> sources = await geoCoder.GetAddressesForPositionAsync(_position);
            List<string> addresses = new List<string>(sources);

            if (addresses.Count > 1)
            {
                DireccionFoto = addresses[0];
            }

            IsRunning = true;
            IsEnabled = false;

            string url = App.Current.Resources["UrlAPI"].ToString();
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                IsRunning = true;
                IsEnabled = false;
                await App.Current.MainPage.DisplayAlert(
                    "Error",
                    "Revise su conexión a Internet",
                    "Aceptar");
                return;
            }


            byte[] ImageArray = null;
            if (File != null)
            {
                ImageArray = _filesHelper.ReadFully(File.GetStream());
                File.Dispose();
            }

            UsuarioAppResponse User = JsonConvert.DeserializeObject<UsuarioAppResponse>(Settings.UsuarioLogueado);

            string Obs = Obra.OBSERVACIONES;
            if (!string.IsNullOrEmpty(Remarks))
            {
                Obs = Remarks;
            }

            DateTime FechaYa = DateTime.Now;

            ObrasDocumentoRequest obrasDocumento = new ObrasDocumentoRequest
            {
                ImageArray = ImageArray,
                FECHA = FechaYa,
                IDObrasPostes = ObrasPoste.NROREGISTRO,
                NROOBRA = ObrasPoste.NROOBRA,
                OBSERVACION = Obs,
                Estante = "App",
                GeneradoPor = User.FullName,
                MODULO = Obra.Modulo,
                NroLote = "App",
                Sector = "App",
                Latitud = Latitud,
                Longitud = Longitud,
                DireccionFoto = DireccionFoto,
                FechaHsFoto = FechaYa,
                TipoDeFoto = TFoto.Codigo,
                Obra = Obra,
            };

            ResponseT<object> response = await _apiService.PostAsync(
              url,
              "api",
              "/ObrasDocuments/ObrasDocument2",
              obrasDocumento);


            if (!response.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;

                await App.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Aceptar");
                return;
            }

            IsRunning = false;
            IsEnabled = true;

            //await App.Current.MainPage.DisplayAlert(
            //    "Ok",
            //    "Guardado con éxito!!",
            //    "Aceptar");


            ObrasDocumentoRequest obraDocumentoRequest = (ObrasDocumentoRequest)response.Result;


            ObraDocumentoResponse2 obraDocumentoResponse2 = new ObraDocumentoResponse2
            {
                NROREGISTRO = obraDocumentoRequest.NROREGISTRO,
                LINK = obraDocumentoRequest.LINK
            };


            MedidoresPageViewModel medidoresPageViewModel = MedidoresPageViewModel.GetInstance();
            medidoresPageViewModel.Images.Add(new ObraDocumentoResponse
            {
                NROREGISTRO = obraDocumentoResponse2.NROREGISTRO,
                NROOBRA = Obra.NroObra,
                OBSERVACION = Obra.OBSERVACIONES,
                LINK = obraDocumentoResponse2.LINK,
                FECHA = FechaYa,
                MODULO = Obra.Modulo,
                NroLote = "App",
                Sector = "App",
                Estante = "App",
                GeneradoPor = User.FullName,
                Latitud = Latitud,
                Longitud = Longitud,
                FechaHsFoto = DateTime.Now,
                TipoDeFoto = TFoto.Codigo,
                DireccionFoto = DireccionFoto,
            }
                ); ;

            await _navigationService.GoBackAsync();
        }
    }
}



