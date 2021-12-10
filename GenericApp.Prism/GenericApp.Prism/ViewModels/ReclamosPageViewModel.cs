using GenericApp.Common.Helpers;
using GenericApp.Common.Responses;
using GenericApp.Common.Services;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace GenericApp.Prism.ViewModels
{
    public class ReclamosPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private readonly IFilesHelper _filesHelper;

        private UsuarioAppResponse _user;
        public UsuarioAppResponse User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }

        private UsuarioAppResponse _usuarioLogueado;
        public UsuarioAppResponse UsuarioLogueado
        {
            get => _usuarioLogueado;
            set => SetProperty(ref _usuarioLogueado, value);
        }

        private static ReclamosPageViewModel _instance;

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

        private int _cantReclamos;
        public int CantReclamos
        {
            get => _cantReclamos;
            set => SetProperty(ref _cantReclamos, value);
        }

        private ObservableCollection<ReclamoItemViewModel> _obras;
        public ObservableCollection<ReclamoItemViewModel> Obras
        {
            get => _obras;
            set => SetProperty(ref _obras, value);
        }

        public List<ObrasPosteResponse> MyObras { get; set; }

        private DelegateCommand _addReclamo;
        public DelegateCommand AddReclamo => _addReclamo ?? (_addReclamo = new DelegateCommand(AddAsync));


        private DelegateCommand _refreshCommand;
        public DelegateCommand RefreshCommand => _refreshCommand ?? (_refreshCommand = new DelegateCommand(Refresh));

        public static ReclamosPageViewModel GetInstance()
        {
            return _instance;
        }

        public ReclamosPageViewModel(INavigationService navigationService, IApiService apiService, IFilesHelper filesHelper) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            _filesHelper = filesHelper;
            _instance = this;

            Obra = JsonConvert.DeserializeObject<ObraResponse>(Settings.Obra);
            NroObra = Obra.NroObra;
            LoadUser();
            UsuarioLogueado = JsonConvert.DeserializeObject<UsuarioAppResponse>(Settings.UsuarioLogueado);
            IsEnabled = false;
            Title = "Reclamos";
        }

        private async void AddAsync()
        {
            Settings.Obra = JsonConvert.SerializeObject(this);
            await _navigationService.NavigateAsync("ReclamoPage");
        }

        public async void LoadUser()
        {
            User = JsonConvert.DeserializeObject<UsuarioAppResponse>(Settings.UsuarioLogueado);

            var controller = string.Format("/ObrasPostes/GetReclamos/{0}", NroObra);
            var url = App.Current.Resources["UrlAPI"].ToString();
            IsRunning = true;
            var response = await _apiService.GetObrasPoste(
                url,
                "api",
                controller,
                NroObra);
            IsRefreshing = false;
            IsRunning = false;

            if (!response.IsSuccess)
            {
                IsEnabled = true;
                await App.Current.MainPage.DisplayAlert("Error", "Problema para recuperar datos.", "Aceptar");
                return;
            }
            MyObras = (List<ObrasPosteResponse>)response.Result;
            RefreshList();
            IsRefreshing = false;


        }

        public async void RefreshList()
        {
            var myObraItemViewModel = MyObras.Select(a => new ReclamoItemViewModel(_navigationService)
            {
                ASTICKET = a.ASTICKET,
                CERTIFICADO = a.CERTIFICADO,
                FECHAASIGNACION = a.FECHAASIGNACION,
                NROOBRA = a.NROOBRA,
                NUMERACION = a.NUMERACION,
                ObservacionAdicional = a.ObservacionAdicional,
                TERMINAL = a.TERMINAL,
                ZONA = a.ZONA,
                CajaDAE = a.CajaDAE,
                CausanteC = a.CausanteC,
                Cliente = a.Cliente,
                DIRECCION = a.DIRECCION,
                FechaCarga = a.FechaCarga,
                GRXX = a.GRXX,
                GRYY = a.GRYY,
                IDUsrIn = a.IDUsrIn,
                Lindero1 = a.Lindero1,
                Lindero2 = a.Lindero2,
                Localidad = a.Localidad,
                MES = a.MES,
                NROREGISTRO = a.NROREGISTRO,
                Precinto = a.Precinto,
                RiesgoElectrico = a.RiesgoElectrico,
                SerieMedidorColocado = a.SerieMedidorColocado,
                Subcontratista = a.Subcontratista,
                Telefono = a.Telefono,
                TipoImput = a.TipoImput,
                OBSERVACIONES = a.OBSERVACIONES,
                CantObras = a.CantObras,
            });
            Obras = new ObservableCollection<ReclamoItemViewModel>(myObraItemViewModel
                .OrderBy(o => o.NROREGISTRO));

            CantReclamos = Obras.Count();


        }
        private async void Search()
        {
            RefreshList();
        }

        private async void Refresh()
        {
            LoadUser();
        }
    }
}