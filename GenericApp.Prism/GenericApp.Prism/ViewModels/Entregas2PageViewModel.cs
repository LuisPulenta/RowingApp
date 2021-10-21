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
    public class Entregas2PageViewModel : ViewModelBase
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

        private ObservableCollection<EntregaDetalleResponse> _entregas;
        public ObservableCollection<EntregaDetalleResponse> Entregas
        {
            get => _entregas;
            set => SetProperty(ref _entregas, value);
        }

        private string _filter;
        public string Filter
        {
            get => _filter;
            set => SetProperty(ref _filter, value);
        }

        public List<EntregaDetalleResponse> MyEntregas { get; set; }

        private DelegateCommand _searchCommand;
        private DelegateCommand _refreshCommand;

        public DelegateCommand SearchCommand => _searchCommand ?? (_searchCommand = new DelegateCommand(Search));
        public DelegateCommand RefreshCommand => _refreshCommand ?? (_refreshCommand = new DelegateCommand(Refresh));

        private static Entregas2PageViewModel _instance;
        public static Entregas2PageViewModel GetInstance()
        {
            return _instance;
        }

        public Entregas2PageViewModel(INavigationService navigationService, IApiService apiService) : base(navigationService)
        {
            Entregas = new ObservableCollection<EntregaDetalleResponse>();
            Causante = JsonConvert.DeserializeObject<CausanteResponse>(Settings.Causante);
            _apiService = apiService;
            _navigationService = navigationService;
            _instance = this;
            LoadEntregas();
            Title = "Entregas por ítem";
        }

        public async void LoadEntregas()
        {
            Causante = JsonConvert.DeserializeObject<CausanteResponse>(Settings.Causante);
            var controller = string.Format("/Entregas/GetEntregas2/{0}", Causante.codigo);
            var url = App.Current.Resources["UrlAPI"].ToString();
            IsRunning = true;
            var response = await _apiService.GetEntregas2ForCodigo(
                url,
                "api",
                controller,
                Causante.codigo);
            IsRefreshing = false;
            IsRunning = false;
            if (!response.IsSuccess)
            {
                IsEnabled = true;
                await App.Current.MainPage.DisplayAlert("Error", "Problema para recuperar datos.", "Aceptar");
                return;
            }
            MyEntregas = (List<EntregaDetalleResponse>)response.Result;
            RefreshList();
            IsRefreshing = false;
        }

        public void RefreshList()
        {
            if (string.IsNullOrEmpty(this.Filter))
            {

                var myEntregaDetalle = MyEntregas.Select(a => new EntregaDetalleResponse()
                {
                    stock_act=a.stock_act,
                    CODIGOSAP = a.CODIGOSAP,
                    causante = a.causante,
                    codigo = a.codigo,
                    Denominacion = a.Denominacion,
                    fecha = a.fecha,
                    grupo = a.grupo,
                    identity_column = a.identity_column
                });
                Entregas = new ObservableCollection<EntregaDetalleResponse>(myEntregaDetalle
                    .OrderBy(o => o.Denominacion));
            }
            else
            {
                var myEntregaDetalle = MyEntregas.Select(a => new EntregaDetalleResponse()
                {
                    stock_act = a.stock_act,
                    CODIGOSAP = a.CODIGOSAP,
                    causante = a.causante,
                    codigo = a.codigo,
                    Denominacion = a.Denominacion,
                    fecha = a.fecha,
                    grupo = a.grupo,
                    identity_column = a.identity_column
                });
                Entregas = new ObservableCollection<EntregaDetalleResponse>(myEntregaDetalle
                    .OrderBy(o => o.Denominacion)
                    .Where(
                            o => (o.Denominacion.ToLower().Contains(this.Filter.ToLower()))
                            
                            )
                          );
            }
        }

        private async void Refresh()
        {
            IsRefreshing = true;
            RefreshList();
            IsRefreshing = false;
        }

        private async void Search()
        {
            IsRefreshing = true;
            RefreshList();
            IsRefreshing = false;
        }
    }
}
