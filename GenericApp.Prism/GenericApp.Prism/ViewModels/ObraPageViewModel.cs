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
    public class ObraPageViewModel : ViewModelBase
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

        private ObraResponse _obra;
        public ObraResponse Obra
        {
            get => _obra;
            set => SetProperty(ref _obra, value);
        }

        private string _modulo;
        public string Modulo
        {
            get => _modulo;
            set => SetProperty(ref _modulo, value);
        }

        private string _observaciones;
        public string Observaciones
        {
            get => _observaciones;
            set => SetProperty(ref _observaciones, value);
        }

        private string _nroObra;
        public string NroObra
        {
            get => _nroObra;
            set => SetProperty(ref _nroObra, value);
        }

        private string _nombreObra;
        public string NombreObra
        {
            get => _nombreObra;
            set => SetProperty(ref _nombreObra, value);
        }

        private string _eLEMPEP;
        public string ELEMPEP
        {
            get => _eLEMPEP;
            set => SetProperty(ref _eLEMPEP, value);
        }

       

        private DelegateCommand _cancelCommand;
        public DelegateCommand CancelCommand => _cancelCommand ?? (_cancelCommand = new DelegateCommand(Cancel));
        
        private DelegateCommand _deletePhotoCommand;
        public DelegateCommand DeletePhotoCommand => _deletePhotoCommand ?? (_deletePhotoCommand = new DelegateCommand(DeletePhotoAsync));
        
        private DelegateCommand _newPhotoCommand;
        public DelegateCommand NewPhotoCommand => _newPhotoCommand ?? (_newPhotoCommand = new DelegateCommand(NewPhotoAsync));

        private DelegateCommand _documentsCommand;
        public DelegateCommand DocumentsCommand => _documentsCommand ?? (_documentsCommand = new DelegateCommand(DocumentsAsync));

        public ObraPageViewModel(INavigationService navigationService, IApiService apiService, IFilesHelper filesHelper) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            _filesHelper = filesHelper;
            UsuarioLogueado = JsonConvert.DeserializeObject<UsuarioAppResponse>(Settings.UsuarioLogueado);
            //Obra = JsonConvert.DeserializeObject<ObraResponse>(Settings.Obra);
            //Images = new ObservableCollection<ObraDocumentoResponse>(Obra.ObrasDocumentos);
            IsEnabled = true;
            //Title = "Obra: "+Obra.NombreObra;
            instance = this;
            ImageSource = "noimage.png";
            

        }

        private async void NewPhotoAsync()
        {
            if (UsuarioLogueado.HabilitaFotos != 1)
            {
                await App.Current.MainPage.DisplayAlert("Aviso!", "Su Usuario no está habilitado para sacar fotos.", "Aceptar");
                return;
            }

            NavigationParameters parameters = new NavigationParameters
            {
                { "obra", this }
            };
            Settings.Obra = JsonConvert.SerializeObject(this);

            await _navigationService.NavigateAsync("TakePhotoPage", parameters);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("obra"))
            {
                Obra = parameters.GetValue<ObraResponse>("obra");
                Title = Obra.NombreObra;
                ImagesTemp = new ObservableCollection<ObraDocumentoResponse>(Obra.ObrasDocumentos);
                Images = new ObservableCollection<ObraDocumentoResponse>();
                IdPhoto = 0;
                if (Images.Count > 0)
                {
                    IdPhoto = Images[0].NROREGISTRO;
                };
                NroObra = Obra.NroObra.ToString();
                NombreObra = Obra.NombreObra;
                ELEMPEP = Obra.ELEMPEP;
                Modulo = Obra.Modulo;
                Observaciones = Obra.OBSERVACIONES;
                foreach(ObraDocumentoResponse obraDocumentoResponse in ImagesTemp)
                {
                    if(obraDocumentoResponse.TipoDeFoto>=0 && obraDocumentoResponse.TipoDeFoto <= 3)
                    {
                        Images.Add(obraDocumentoResponse);
                    }
                }
            }
        }

        #region Singleton

        private static ObraPageViewModel instance;
        public static ObraPageViewModel GetInstance()
        {
            return instance;
        }
        #endregion

        private async void Cancel()
        {
            await _navigationService.GoBackAsync();
        }

        private async void TakePhoto()
        {
            

        }

        private async void DeletePhotoAsync()
        {
                
            if (UsuarioLogueado.HabilitaFotos!=1)
            {
                await App.Current.MainPage.DisplayAlert("Aviso!", "Su Usuario no está habilitado para eliminar fotos.", "Aceptar");
                return;
            }

            if (IdPhoto == 0)
            {
                IsRunning = false;
                IsEnabled = true;
                await App.Current.MainPage.DisplayAlert(
                    "Error",
                    "Desplace las fotos hasta la foto que desea borrar",
                    "Aceptar");
                return;
            }

            if (Images.Count == 1)
            {
                IdPhoto = Images[0].NROREGISTRO;
            }


            var answer = await App.Current.MainPage.DisplayAlert(
                "Confirmar",
                "Está seguro de borrar esta foto?",
                "Si",
                "No");

            if (!answer)
            {
                return;
            }

            IsRunning = true;
            IsEnabled = false;


            string url = App.Current.Resources["UrlAPI"].ToString();
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                IsRunning = false;
                IsEnabled = true;
                await App.Current.MainPage.DisplayAlert(
                    "Error",
                    "Revise su conexión a Internet",
                    "Aceptar");
                return;
            }

            var response = await _apiService.DeleteAsync(
            url,
            "api",
            "/ObrasDocuments",
            IdPhoto);

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
            IdPhoto = 0;
            await App.Current.MainPage.DisplayAlert(
                "Ok",
                "Foto eliminada con éxito!!",
                "Aceptar");

            Images.Remove(Images[IdPhotoNro]);

            ObrasPageViewModel obrasPageViewModel = ObrasPageViewModel.GetInstance();
            obrasPageViewModel.LoadUser();
            obrasPageViewModel.RefreshList();

            if (IdPhotoNro == Images.Count)
            {
                IdPhotoNro = 0;
            }



            if (Images.Count == 0)
            {
                IdPhoto = 0;
                return;
            }

            if (Images.Count == 1)
            {
                IdPhotoNro = 0;
                IdPhoto = Images[0].NROREGISTRO;
            }
            else
            {
                IdPhoto = Images[IdPhotoNro].NROREGISTRO;
            };
        }

        private async void DocumentsAsync()
        {
            NavigationParameters parameters = new NavigationParameters
            {
                { "obra", this }
            };
            //Settings.Obra = JsonConvert.SerializeObject(this);
            await _navigationService.NavigateAsync("DocumentsPage", parameters);
        }
    }
}