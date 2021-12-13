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

        private DelegateCommand _saveCommand;
        public DelegateCommand SaveCommand => _saveCommand ?? (_saveCommand = new DelegateCommand(SaveAsync));

        private DelegateCommand _deletePhotoCommand;
        public DelegateCommand DeletePhotoCommand => _deletePhotoCommand ?? (_deletePhotoCommand = new DelegateCommand(DeletePhotoAsync));

        private DelegateCommand _newPhotoCommand;
        public DelegateCommand NewPhotoCommand => _newPhotoCommand ?? (_newPhotoCommand = new DelegateCommand(NewPhotoAsync));



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

            
            IsRunning = true;
            var controller = string.Format("/ObrasDocuments/GetObrasDocumentos/{0}", response.Result.NROREGISTRO);
            var response2 = await _apiService.GetObrasDocumentosAsync(url, "API", controller, response.Result.NROREGISTRO);
            IsRunning = false;

            if (!response2.IsSuccess)
            {
                MyObrasDocumentos = null;
            }
            else
            {
                MyObrasDocumentos = (List<ObraDocumentoResponse>)response2.Result;
            }

            if (MyObrasDocumentos != null)
            {
                var myListObrasDocumentos = MyObrasDocumentos.Select(a => new ObraDocumentoResponse()
                {
                    FECHA = a.FECHA,
                    NROOBRA = a.NROOBRA,
                    IDObrasPostes=a.IDObrasPostes,
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
                    if (obraDocumentoResponse.TipoDeFoto >= 4 && obraDocumentoResponse.TipoDeFoto <= 9)
                    {
                        Images.Add(obraDocumentoResponse);
                    }
                }
                var abc = 1;
            }
        }

        #region Singleton

        private static MedidoresPageViewModel instance;
        public static MedidoresPageViewModel GetInstance()
        {
            return instance;
        }
        #endregion

        private async void SaveAsync()
        {
            if(!string.IsNullOrEmpty(ObrasPoste.SerieMedidorColocado))
            {
                if (ObrasPoste.SerieMedidorColocado.Length > 20)
                {
                    await App.Current.MainPage.DisplayAlert("Error", "El N° de Serie del Medidor colocado no puede tener más de 20 caracteres.", "Aceptar");
                    return;
                }
            }

            if (!string.IsNullOrEmpty(ObrasPoste.Precinto))
            {
                if (ObrasPoste.Precinto.Length > 20)
                {
                    await App.Current.MainPage.DisplayAlert("Error", "El Precinto no puede tener más de 20 caracteres.", "Aceptar");
                    return;
                }
            }

            if (!string.IsNullOrEmpty(ObrasPoste.CajaDAE))
            {
                if (ObrasPoste.CajaDAE.Length > 20)
                {
                    await App.Current.MainPage.DisplayAlert("Error", "La Caja DAE no puede tener más de 20 caracteres.", "Aceptar");
                    return;
                }
            }

            if (!string.IsNullOrEmpty(ObrasPoste.Lindero1))
            {
                if (ObrasPoste.Lindero1.Length > 50)
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Lindero 1 no puede tener más de 50 caracteres.", "Aceptar");
                    return;
                }
            }

            if (!string.IsNullOrEmpty(ObrasPoste.Lindero2))
            {
                if (ObrasPoste.Lindero2.Length > 50)
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Lindero 2 no puede tener más de 50 caracteres.", "Aceptar");
                    return;
                }
            }


            //Verificar conectividad
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                IsRunning = false;
                IsEnabled = true;
                await App.Current.MainPage.DisplayAlert(
                    "Error",
                    "Error de conexión",
                    "Aceptar");
                return;
            }

            //****************************************************************************************************************
            IsRunning = true;
            IsEnabled = false;


            //*********************************************************************************************************
            //Grabar 
            //*********************************************************************************************************
            string url = App.Current.Resources["UrlAPI"].ToString();

            var myMedidor = new ObrasPosteRequest
                    {
                        ASTICKET= ObrasPoste.ASTICKET,
                        CERTIFICADO = ObrasPoste.CERTIFICADO,
                        NROOBRA = ObrasPoste.NROOBRA,
                        NUMERACION = ObrasPoste.NUMERACION,
                        OBSERVACIONES = ObrasPoste.OBSERVACIONES,
                        CajaDAE = ObrasPoste.CajaDAE,
                        Cliente = ObrasPoste.Cliente,
                        DIRECCION = ObrasPoste.DIRECCION,
                        Lindero1 = ObrasPoste.Lindero1,
                        Lindero2 = ObrasPoste.Lindero2,
                        Localidad = ObrasPoste.Localidad,
                        NROREGISTRO = ObrasPoste.NROREGISTRO,
                        Precinto = ObrasPoste.Precinto,
                        SerieMedidorColocado = ObrasPoste.SerieMedidorColocado,
                        Telefono = ObrasPoste.Telefono,
                        TipoImput = ObrasPoste.TipoImput,
                ZONA = ObrasPoste.ZONA,
                TERMINAL = ObrasPoste.TERMINAL,
                FECHAASIGNACION = ObrasPoste.FECHAASIGNACION,
                Subcontratista = ObrasPoste.Subcontratista,
                RiesgoElectrico = ObrasPoste.RiesgoElectrico,
                FechaCarga = ObrasPoste.FechaCarga,
                CausanteC = ObrasPoste.CausanteC,
                IDUsrIn = ObrasPoste.IDUsrIn,
                MES = ObrasPoste.MES,
                GRXX = ObrasPoste.GRXX,
                GRYY = ObrasPoste.GRYY,
                ObservacionAdicional = ObrasPoste.ObservacionAdicional,

            };

                    var response = await _apiService.PutAsync2(
                    url,
                    "api",
                    "/ObrasPostes",
                    myMedidor,
                    myMedidor.NROREGISTRO);

                    IsRunning = false;
                    IsEnabled = true;

                    if (!response.IsSuccess)
                    {
                        await App.Current.MainPage.DisplayAlert("Error", "Ocurrió un error al guardar los cambios, intente más tarde.", "Aceptar");
                        return;
                    }
                
               
                await App.Current.MainPage.DisplayAlert("Ok", "Cambios guardados con éxito!!", "Aceptar");
                //await _navigationService.GoBackAsync();
                return;
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

            await _navigationService.NavigateAsync("TakePhoto2Page", parameters);
        }

        private async void DeletePhotoAsync()
        {

            if (UsuarioLogueado.HabilitaFotos != 1)
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
    }
}