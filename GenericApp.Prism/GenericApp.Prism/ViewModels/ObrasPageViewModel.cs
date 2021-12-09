using GenericApp.Common.Helpers;
using GenericApp.Common.Responses;
using GenericApp.Common.Services;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace GenericApp.Prism.ViewModels
{
    public class ObrasPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        
        private UsuarioAppResponse _user;
        public UsuarioAppResponse User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }
        
        private static ObrasPageViewModel _instance;
        
        private string _filter;
        public string Filter
        {
            get => _filter;
            set => SetProperty(ref _filter, value);
        }
       
        private int _cantObras;
        public int CantObras
        {
            get => _cantObras;
            set => SetProperty(ref _cantObras, value);
        }

        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => SetProperty(ref _isRefreshing, value);
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

        private ObraDocumentoResponse _obrasFotos;
        public ObraDocumentoResponse ObrasFotos
        {
            get => _obrasFotos;
            set => SetProperty(ref _obrasFotos, value);
        }

        private ObservableCollection<ObraItemViewModel> _obras;
        public ObservableCollection<ObraItemViewModel> Obras
        {
            get => _obras;
            set => SetProperty(ref _obras, value);
        }

        public List<ObraResponse> MyObras { get; set; }

        private DelegateCommand _searchCommand;
        private DelegateCommand _refreshCommand;

        public DelegateCommand SearchCommand => _searchCommand ?? (_searchCommand = new DelegateCommand(Search));
        public DelegateCommand RefreshCommand => _refreshCommand ?? (_refreshCommand = new DelegateCommand(Refresh));

        public static ObrasPageViewModel GetInstance()
        {
            return _instance;
        }


        public ObrasPageViewModel(INavigationService navigationService, IApiService apiService) : base(navigationService)
        {
            _apiService = apiService;
            _navigationService = navigationService;
            LoadUser();
            Obras = new ObservableCollection<ObraItemViewModel>();
            
            if (User.Modulo == "ObrasTasa")
            {
                Title = "Obras Tasa";
            }
            else
            {
                Title = "Obras " + User.Modulo;
            }
            
            _instance = this;
        }


        public async void LoadUser()
        {
            User = JsonConvert.DeserializeObject<UsuarioAppResponse>(Settings.UsuarioLogueado);

            var controller = "";

            if (User.Modulo== "Energia")
            {
                controller = string.Format("/Account/GetObrasEnergia");
            }
            else if (User.Modulo == "ObrasTasa")
            {
                controller = string.Format("/Account/GetObrasObrasTasa");
            }
            else if (User.Modulo == "Rowing")
            {
                controller = string.Format("/Account/GetObrasRowing");
            };

            var url = App.Current.Resources["UrlAPI"].ToString();
            IsRunning = true;
            var response = await _apiService.GetObras(
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
            MyObras = (List<ObraResponse>)response.Result;
            RefreshList();
            IsRefreshing = false;

            //Remotes = new ObservableCollection<RemoteItemViewModel>(MyRemotes.Select(a => new RemoteItemViewModel(_navigationService)
            //{
            //}).ToList());
        }

        public void RefreshList()
        {

            if (string.IsNullOrEmpty(this.Filter))
            {

                var myObraItemViewModel = MyObras.Select(a => new ObraItemViewModel(_navigationService)
                {
                    ELEMPEP=a.ELEMPEP,
                    NombreObra=a.NombreObra,
                    NroObra=a.NroObra,
                    CantObras=a.CantObras,
                    OBSERVACIONES=a.OBSERVACIONES,
                    Modulo=a.Modulo,
                    ObrasDocumentos=a.ObrasDocumentos
                    
                });
                Obras = new ObservableCollection<ObraItemViewModel>(myObraItemViewModel
                    .OrderBy(o => o.NombreObra));

                CantObras = Obras.Count();
            }
            else
            {
                var myObraItemViewModel = MyObras.Select(a => new ObraItemViewModel(_navigationService)
                {
                    ELEMPEP = a.ELEMPEP,
                    NombreObra = a.NombreObra,
                    NroObra = a.NroObra,
                    CantObras = a.CantObras,
                    OBSERVACIONES = a.OBSERVACIONES,
                    Modulo = a.Modulo,
                    ObrasDocumentos = a.ObrasDocumentos
                });
                Obras = new ObservableCollection<ObraItemViewModel>(myObraItemViewModel
                    .OrderBy(o => o.NombreObra)
                    .Where(
                            o => (o.NombreObra.ToLower().Contains(this.Filter.ToLower()))
                            ||
                            (o.ELEMPEP.ToLower().Contains(this.Filter.ToLower()))
                            ||
                            (o.NroObra.ToString().Contains(this.Filter.ToLower())))
                          );
                CantObras = Obras.Count();
            }
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
