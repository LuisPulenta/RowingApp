using GenericApp.Common.Helpers;
using GenericApp.Common.Responses;
using GenericApp.Common.Services;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Essentials;

namespace GenericApp.Prism.ViewModels
{
    public class MedidoresPageViewModel : ViewModelBase
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

        private ObrasPosteResponse _obrasPoste;
        public ObrasPosteResponse ObrasPoste
        {
            get => _obrasPoste;
            set => SetProperty(ref _obrasPoste, value);
        }

        public string Ticket { get; set; }

        private string _domicilio;
        public string Domicilio
        {
            get => _domicilio;
            set => SetProperty(ref _domicilio, value);
        }

        private DelegateCommand _consultarCommand;
        public DelegateCommand ConsultarCommand => _consultarCommand ?? (_consultarCommand = new DelegateCommand(ConsultarAsync));

        public MedidoresPageViewModel(INavigationService navigationService, IApiService apiService) : base(navigationService)
        {
            _apiService = apiService;
            _navigationService = navigationService;
            Title = "Medidores";
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

            var response = await _apiService.GetTicketAsync(url, "api", "/ObrasPostes/GetTicket", Ticket);

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
        }
    }
}
