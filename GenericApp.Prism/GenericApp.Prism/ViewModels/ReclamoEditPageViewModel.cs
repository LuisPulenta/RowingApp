using GenericApp.Common.Helpers;
using GenericApp.Common.Requests;
using GenericApp.Common.Responses;
using GenericApp.Common.Services;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using System;
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

        public ReclamoEditPageViewModel(INavigationService navigationService, IApiService apiService, IFilesHelper filesHelper) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            _filesHelper = filesHelper;
            Obra = JsonConvert.DeserializeObject<ObraResponse>(Settings.Obra);
            NroObra = Obra.NroObra;
            UsuarioLogueado = JsonConvert.DeserializeObject<UsuarioAppResponse>(Settings.UsuarioLogueado);
            IsEnabled = false;
            Title = "Reclamo Materiales";
        }

    }
}