using GenericApp.Common.Helpers;
using GenericApp.Common.Requests;
using GenericApp.Common.Responses;
using GenericApp.Common.Services;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Essentials;

namespace GenericApp.Prism.ViewModels
{
    public class ReclamosPageViewModel : ViewModelBase
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

        private ObraResponse _obra;
        public ObraResponse Obra
        {
            get => _obra;
            set => SetProperty(ref _obra, value);
        }

        private int _nroObra;
        public int NroObra
        {
            get => _nroObra;
            set => SetProperty(ref _nroObra, value);
        }

        private string _zona;
        public string Zona
        {
            get => _zona;
            set => SetProperty(ref _zona, value);
        }

        private string _descripcion;
        public string Descripcion
        {
            get => _descripcion;
            set => SetProperty(ref _descripcion, value);
        }

        private string _direccion;
        public string Direccion
        {
            get => _direccion;
            set => SetProperty(ref _direccion, value);
        }

        private string _numero;
        public string Numero
        {
            get => _numero;
            set => SetProperty(ref _numero, value);
        }

        private string _nroReclamo;
        public string NroReclamo
        {
            get => _nroReclamo;
            set => SetProperty(ref _nroReclamo, value);
        }

        private DelegateCommand _saveCommand;
        public DelegateCommand SaveCommand => _saveCommand ?? (_saveCommand = new DelegateCommand(SaveAsync));

        public ReclamosPageViewModel(INavigationService navigationService, IApiService apiService, IFilesHelper filesHelper) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            _filesHelper = filesHelper;
            UsuarioLogueado = JsonConvert.DeserializeObject<UsuarioAppResponse>(Settings.UsuarioLogueado);
            IsEnabled = false;
            Title = "Reclamos";

        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("obra"))
            {
                Obra = parameters.GetValue<ObraResponse>("obra");
                NroObra = Obra.NroObra;
            }
        }

        private async void SaveAsync()
        {
            if (string.IsNullOrEmpty(Zona))
            {
                await App.Current.MainPage.DisplayAlert(
                    "Error",
                    "Debe ingresar una Zona",
                    "Aceptar");
                return;
            }

            if (Zona.Length > 50)
            {
                await App.Current.MainPage.DisplayAlert("Error", "La zona no puede tener más de 50 caracteres.", "Aceptar");
                return;
            }

            if (string.IsNullOrEmpty(Descripcion))
            {
                await App.Current.MainPage.DisplayAlert(
                    "Error",
                    "Debe ingresar una Descripción",
                    "Aceptar");
                return;
            }

            if (Descripcion.Length > 160)
            {
                await App.Current.MainPage.DisplayAlert("Error", "La Descripción no puede tener más de 160 caracteres.", "Aceptar");
                return;
            }

            if (string.IsNullOrEmpty(Direccion))
            {
                await App.Current.MainPage.DisplayAlert(
                    "Error",
                    "Debe ingresar una Dirección",
                    "Aceptar");
                return;
            }

            if (Direccion.Length > 100)
            {
                await App.Current.MainPage.DisplayAlert("Error", "La Dirección no puede tener más de 100 caracteres.", "Aceptar");
                return;
            }

            if (string.IsNullOrEmpty(Numero))
            {
                await App.Current.MainPage.DisplayAlert(
                    "Error",
                    "Debe ingresar un Número",
                    "Aceptar");
                return;
            }

            if (Numero.Length > 19)
            {
                await App.Current.MainPage.DisplayAlert("Error", "El Número no puede tener más de 19 caracteres.", "Aceptar");
                return;
            }

            if (string.IsNullOrEmpty(NroReclamo))
            {
                await App.Current.MainPage.DisplayAlert(
                    "Error",
                    "Debe ingresar un AS/N° Reclamo",
                    "Aceptar");
                return;
            }

            if (NroReclamo.Length > 20)
            {
                await App.Current.MainPage.DisplayAlert("Error", "El AS/N° Reclamo no puede tener más de 20 caracteres.", "Aceptar");
                return;
            }

            if (UsuarioLogueado.CODIGOCAUSANTE.Length<1)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Su Usuario no tiene cargado el campo CODIGOCAUSANTE", "Aceptar");
                return;
            }

            if (UsuarioLogueado.CODIGOGRUPO.Length < 1)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Su Usuario no tiene cargado el campo CODIGOGRUPO", "Aceptar");
                return;
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

            var myReclamo = new ObrasPosteRequest
            {
                ASTICKET = NroReclamo,
                NROOBRA = NroObra,
                NUMERACION = Numero,
                DIRECCION = Direccion,
                CODIGOCAUSANTE= UsuarioLogueado.CODIGOCAUSANTE,
                CODIGOGRUPO=UsuarioLogueado.CODIGOGRUPO,
                TERMINAL=Descripcion,
                ZONA=Zona,
                TipoImput = "Reclamos",
            };

            var response = await _apiService.PostAsync(
            url,
            "api",
            "/ObrasPostes/PostReclamo",
            myReclamo);

            IsRunning = false;
            IsEnabled = true;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Ocurrió un error al guardar el Reclamo, intente más tarde.", "Aceptar");
                return;
            }

            await App.Current.MainPage.DisplayAlert("Ok", "Reclamo guardado con éxito!!", "Aceptar");
            await _navigationService.GoBackAsync();
            return;
        }
    }
}
