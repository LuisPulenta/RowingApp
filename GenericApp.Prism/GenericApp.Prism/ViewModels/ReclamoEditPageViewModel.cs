using GenericApp.Common.Helpers;
using GenericApp.Common.Requests;
using GenericApp.Common.Responses;
using GenericApp.Common.Services;
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
    public class ReclamoEditPageViewModel : ViewModelBase
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

        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => SetProperty(ref _isRefreshing, value);
        }

        private ObrasPosteResponse _obra;
        public ObrasPosteResponse Obra
        {
            get => _obra;
            set => SetProperty(ref _obra, value);
        }

        public List<CatalogoResponse> MyCatalogos { get; set; }

        private ObservableCollection<CatalogoItemViewModel> _catalogos;
        public ObservableCollection<CatalogoItemViewModel> Catalogos
        {
            get => _catalogos;
            set => SetProperty(ref _catalogos, value);
        }

        private DelegateCommand _saveCommand;
        public DelegateCommand SaveCommand => _saveCommand ?? (_saveCommand = new DelegateCommand(SaveAsync));


        public ReclamoEditPageViewModel(INavigationService navigationService, IApiService apiService, IFilesHelper filesHelper) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            _filesHelper = filesHelper;
            Obra = JsonConvert.DeserializeObject<ObrasPosteResponse>(Settings.ObrasPoste);
            UsuarioLogueado = JsonConvert.DeserializeObject<UsuarioAppResponse>(Settings.UsuarioLogueado);
            LoadCatalogos();
            IsEnabled = false;
            Title = "Reclamo Materiales";
        }

        public async void LoadCatalogos()
        {
            User = JsonConvert.DeserializeObject<UsuarioAppResponse>(Settings.UsuarioLogueado);

            var controller = "";

            if (User.Modulo == "Energia")
            {
                controller = string.Format("/Catalogos/GetCatalogosEnergia");
            }
            else if (User.Modulo == "ObrasTasa")
            {
                controller = string.Format("/Catalogos/GetCatalogosObrasTasa");
            }
            else if (User.Modulo == "Rowing")
            {
                controller = string.Format("/Catalogos/GetCatalogosRowing");
            };

            var url = App.Current.Resources["UrlAPI"].ToString();
            IsRunning = true;
            var response = await _apiService.GetCatalogos(
                url,
                "api",
                controller);
            IsRefreshing = false;
            IsRunning = false;
            if (!response.IsSuccess)
            {
                IsEnabled = true;
                await App.Current.MainPage.DisplayAlert("Error", "Problema para recuperar datos.", "Aceptar");
                return;
            }
            MyCatalogos = (List<CatalogoResponse>)response.Result;
            RefreshList();
            IsRefreshing = false;
        }

        public void RefreshList()
        {
         
                var myCatalogoItemViewModel = MyCatalogos.Select(a => new CatalogoItemViewModel(_navigationService)
                {
                    VerEnReclamosApp=a.VerEnReclamosApp,
                    catCatalogo=a.catCatalogo,
                    catCodigo=a.catCodigo,
                    CodigoSap=a.CodigoSap,
                    Modulo = a.Modulo,
                    Cantidad=null,
                });
                Catalogos = new ObservableCollection<CatalogoItemViewModel>(myCatalogoItemViewModel
                    .OrderBy(o => o.catCatalogo));
           
        }

        private async void SaveAsync()
        {



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

            bool bandera = false;

            foreach (var myCatalogo in MyCatalogos)
            {
                if (myCatalogo.Cantidad > 0)
                {
                    bandera = true;
                    var myObrasPostesCajaDetalle = new ObrasPostesCajaDetalleResponse
                    {
                        CANTIDAD = (decimal)myCatalogo.Cantidad,
                        CATCODIGO = myCatalogo.catCodigo,
                        NROREGISTROCAB = Obra.NROREGISTRO,
                        CODIGOSAP = myCatalogo.CodigoSap,
                        NROREGISTROD = 1,
                    };

                    var response = await _apiService.PutAsync2(
                    url,
                    "api",
                    "/ObrasPostes",
                    myObrasPostesCajaDetalle);

                    IsRunning = false;
                    IsEnabled = true;

                    if (!response.IsSuccess)
                    {
                        await App.Current.MainPage.DisplayAlert("Error", "Ocurrió un error al guardar los cambios, intente más tarde.", "Aceptar");
                        return;
                    }
                }
            }

            if (bandera == false)
            {
                await App.Current.MainPage.DisplayAlert("Ok", "No hay materiales que tengan cantidades", "Aceptar");
                return;
            }

            await App.Current.MainPage.DisplayAlert("Ok", "Materiales guardados con éxito!!", "Aceptar");
            await _navigationService.GoBackAsync();
            return;
        }

        private async void Search()
        {
            RefreshList();
        }

        private async void Refresh()
        {
            LoadCatalogos();
        }

    }
}