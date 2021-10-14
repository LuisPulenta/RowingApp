using GenericApp.Common.Helpers;
using GenericApp.Common.Responses;
using GenericApp.Common.Services;
using GenericApp.Prism.Views;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using System;
using Xamarin.Essentials;

namespace GenericApp.Prism.ViewModels
{
    public class SegHigPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private UsuarioAppResponse _user;
        public UsuarioAppResponse User
        {
            get => _user;
            set => SetProperty(ref _user, value);
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

        private bool _isEnabled2;
        public bool IsEnabled2
        {
            get => _isEnabled2;
            set => SetProperty(ref _isEnabled2, value);
        }

        public string Legajo { get; set; }

        private CausanteResponse _causante;
        public CausanteResponse Causante
        {
            get => _causante;
            set => SetProperty(ref _causante, value);
        }

        private DelegateCommand _consultarCommand;
        public DelegateCommand ConsultarCommand => _consultarCommand ?? (_consultarCommand = new DelegateCommand(ConsultarAsync));

        private DelegateCommand _entregasCommand;
        public DelegateCommand EntregasCommand => _entregasCommand ?? (_entregasCommand = new DelegateCommand(EntregasAsync));

        private DelegateCommand _informesCommand;
        public DelegateCommand InformesCommand => _informesCommand ?? (_informesCommand = new DelegateCommand(InformesAsync));

        public SegHigPageViewModel(INavigationService navigationService, IApiService apiService) : base(navigationService)
        {
            _apiService = apiService;
            _navigationService = navigationService;
            Title = "Seguridad e Higiene";
            IsEnabled2 = true;
        }

        private async void ConsultarAsync()
        {
            if (string.IsNullOrEmpty(Legajo))
            {
                await App.Current.MainPage.DisplayAlert(
                    "Error",
                    "Debe ingresar un Legajo",
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

            var response = await _apiService.GetCausanteByCodigoAsync(url, "api", "/Causantes/GetCausanteByCodigo", Legajo);

            if (!response.IsSuccess)
            {
                IsEnabled = false;
                IsRunning = false;
                await App.Current.MainPage.DisplayAlert("Error", "El Legajo ingresado no corresponde a ningún empleado.", "Aceptar");
                Causante = null;

                return;
            }

            Settings.Causante = JsonConvert.SerializeObject(response.Result);
            IsEnabled = true;
            IsRunning = false;
            Causante = response.Result;
        }

        private async void EntregasAsync()
        {
            await _navigationService.NavigateAsync(nameof(EntregasPage));
        }

        private async void InformesAsync()
        {
            await _navigationService.NavigateAsync(nameof(InformesPage));
        }
    }
}
