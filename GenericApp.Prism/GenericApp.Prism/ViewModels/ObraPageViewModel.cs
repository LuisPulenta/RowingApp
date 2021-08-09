using GenericApp.Common.Helpers;
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
    public class ObraPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private readonly IFilesHelper _filesHelper;

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

        private ObraResponse _obra;
        public ObraResponse Obra
        {
            get => _obra;
            set => SetProperty(ref _obra, value);
        }

        private string _observaciones;
        public string Observaciones
        {
            get => _observaciones;
            set => SetProperty(ref _observaciones, value);
        }

        private DelegateCommand _takePhotoCommand;
        public DelegateCommand TakePhotoCommand => _takePhotoCommand ?? (_takePhotoCommand = new DelegateCommand(TakePhoto));
        private DelegateCommand _cancelCommand;
        public DelegateCommand CancelCommand => _cancelCommand ?? (_cancelCommand = new DelegateCommand(Cancel));
        private DelegateCommand _saveCommand;
        public DelegateCommand SaveCommand => _saveCommand ?? (_saveCommand = new DelegateCommand(Save));
        private DelegateCommand _getAddressCommand;

        public ObraPageViewModel(INavigationService navigationService, IApiService apiService, IFilesHelper filesHelper) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            _filesHelper = filesHelper;
            Obra = JsonConvert.DeserializeObject<ObraResponse>(Settings.Obra);
            IsEnabled = true;
            Title = "Obra: "+Obra.NombreObra;
            instance = this;
            ImageSource = "noimage.png";

        }

        #region Singleton

        private static ObraPageViewModel instance;
        public static ObraPageViewModel GetInstance()
        {
            return instance;
        }
        #endregion

        private async void Save()
        {
            if (_file == null)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Debe cargar la foto de la Obra.", "Aceptar");
                return;
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
            

            ObrasDocumentoRequest obrasDocumento = new ObrasDocumentoRequest
            {
                FECHA = DateTime.Now,
                NROOBRA=Obra.NroObra,
                OBSERVACION=Observaciones,
                MODULO="App",
                Estante="App",
                GeneradoPor=User.FullName,
                NroLote="App",
                Sector="App",
                PhotoArray = ImageArray,
            };

            ResponseT<object> response = await _apiService.PostAsync(
            url,
            "api",
            "/ObrasDocumentos",
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

            await App.Current.MainPage.DisplayAlert(
                "Ok",
                "Guardado con éxito!!",
                "Aceptar");

            ObrasPageViewModel obrasPageViewModel = ObrasPageViewModel.GetInstance();
            obrasPageViewModel.RefreshList();

            await _navigationService.GoBackAsync();
        }

        private async void Cancel()
        {
            await _navigationService.GoBackAsync();
        }

        private async void TakePhoto()
        {
            await CrossMedia.Current.Initialize();
            _file = await CrossMedia.Current.TakePhotoAsync(
                new StoreCameraMediaOptions
                {
                    Directory = "Sample",
                    Name = "test.jpg",
                    PhotoSize = PhotoSize.Small,
                }
            );
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
    }
}