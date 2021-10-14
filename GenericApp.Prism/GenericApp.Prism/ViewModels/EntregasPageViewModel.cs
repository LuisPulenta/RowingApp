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
	public class EntregasPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;

        private CausanteResponse _causante;
        public CausanteResponse Causante
        {
            get => _causante;
            set => SetProperty(ref _causante, value);
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

        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => SetProperty(ref _isRefreshing, value);
        }

        private ObservableCollection<EntregaItemViewModel> _entregas;
        public ObservableCollection<EntregaItemViewModel> Entregas
        {
            get => _entregas;
            set => SetProperty(ref _entregas, value);
        }

        public List<EntregaResponse> MyEntregas { get; set; }

        public EntregasPageViewModel(INavigationService navigationService, IApiService apiService) : base(navigationService)
        {
            Entregas = new ObservableCollection<EntregaItemViewModel>();
            _apiService = apiService;
            _navigationService = navigationService;
            LoadEntregas();
            Title = "Entregas";
        }

        public async void LoadEntregas()
        {
            Causante = JsonConvert.DeserializeObject<CausanteResponse>(Settings.Causante);
            var controller = string.Format("/Entregas/GetEntregas/{0}",Causante.codigo);
            var url = App.Current.Resources["UrlAPI"].ToString();
            var response = await _apiService.GetEntregasForCodigo(
                url,
                "api",
                controller,
                Causante.codigo);
            IsRefreshing = false;
            if (!response.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;
                await App.Current.MainPage.DisplayAlert("Error", "Problema para recuperar datos.", "Aceptar");
                return;
            }
            MyEntregas = (List<EntregaResponse>)response.Result;
            RefreshList();
            IsRefreshing = false;
        }

        public void RefreshList()
        {
                var myListEntregaItemViewModel = MyEntregas.Select(a => new EntregaItemViewModel(_navigationService)
                {
                    CantItems = a.CantItems,
                    fecha=a.fecha
                });
                Entregas = new ObservableCollection<EntregaItemViewModel>(myListEntregaItemViewModel.
                    OrderByDescending(o => o.fecha));
        }
    }
}
