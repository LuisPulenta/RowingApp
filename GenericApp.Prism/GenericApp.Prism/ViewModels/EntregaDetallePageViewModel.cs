using GenericApp.Common.Helpers;
using GenericApp.Common.Requests;
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

        public List<EntregaDetalleResponse> MyEntregaDetalles { get; set; }

        private ObservableCollection<EntregaDetalleResponse> _entregaDetalles;
        public ObservableCollection<EntregaDetalleResponse> EntregaDetalles
        {
            get => _entregaDetalles;
            set => SetProperty(ref _entregaDetalles, value);
        }

        public EntregaDetallePageViewModel(INavigationService navigationService, IApiService apiService) : base(navigationService)
        {
            _apiService = apiService;
            _navigationService = navigationService;
            Causante = JsonConvert.DeserializeObject<CausanteResponse>(Settings.Causante);
            Entrega = JsonConvert.DeserializeObject<EntregaResponse>(Settings.Entrega);
            LoadEntregaDetalles();
            Title = "Detalle de Entrega";
        }

        public async void LoadEntregaDetalles()
        {
            Causante = JsonConvert.DeserializeObject<CausanteResponse>(Settings.Causante);
            var controller = "/Entregas/GetEntregaDetalles";
            var url = App.Current.Resources["UrlAPI"].ToString();
            IsRunning = true;
            EntregaDetallesRequest request = new EntregaDetallesRequest();
            request.causante = Causante.codigo;
            request.fecha = Entrega.fecha;
            Response response = await _apiService.GetEntregaDetallesPorFecha(
                url,
                "api",
                controller,
                request);
            IsRefreshing = false;
            IsRunning = false;
            if (!response.IsSuccess)
            {
                IsEnabled = true;
                await App.Current.MainPage.DisplayAlert("Error", "Problema para recuperar datos.", "Aceptar");
                return;
            }
            MyEntregaDetalles = (List<EntregaDetalleResponse>)response.Result;
            RefreshList();
            IsRefreshing = false;
        }

        public void RefreshList()
        {
            var myListEntregaDetalles = MyEntregaDetalles.Select(a => new EntregaDetalleResponse()
            {
                CODIGOSAP = a.CODIGOSAP,
                causante = a.causante,
                codigo = a.codigo,
                Denominacion = a.Denominacion,
                grupo = a.grupo,
                identity_column = a.identity_column,
                fecha = a.fecha,
                stock_act = a.stock_act
            });
            EntregaDetalles = new ObservableCollection<EntregaDetalleResponse>(myListEntregaDetalles.
                OrderBy(o => o.Denominacion));
        }

        private async void Refresh()
        {
            IsRefreshing = true;
            RefreshList();
            IsRefreshing = false;
        }
    }
}
