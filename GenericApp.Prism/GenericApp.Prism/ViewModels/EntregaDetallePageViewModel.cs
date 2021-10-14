using GenericApp.Common.Helpers;
using GenericApp.Common.Responses;
using GenericApp.Common.Services;
using GenericApp.Prism.ItemVIewModels;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Essentials;

namespace GenericApp.Prism.ViewModels
{
    public class EntregaDetallePageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;

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

        private EntregaResponse _entrega;
        public EntregaResponse Entrega
        {
            get => _entrega;
            set => SetProperty(ref _entrega, value);
        }

        private CausanteResponse _causante;
        public CausanteResponse Causante
        {
            get => _causante;
            set => SetProperty(ref _causante, value);
        }

        public EntregaDetallePageViewModel(INavigationService navigationService, IApiService apiService) : base(navigationService)
        {
            _apiService = apiService;
            _navigationService = navigationService;
            Causante = JsonConvert.DeserializeObject<CausanteResponse>(Settings.Causante);
            Entrega = JsonConvert.DeserializeObject<EntregaResponse>(Settings.Entrega);
            Title = "Detalle de Entrega";
        }
    }
}
