using GenericApp.Common.Helpers;
using GenericApp.Common.Responses;
using GenericApp.Common.Services;
using Newtonsoft.Json;
using Prism.Navigation;

namespace GenericApp.Prism.ViewModels
{
    public class InspeccionesPageViewModel : ViewModelBase
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

        public InspeccionesPageViewModel(INavigationService navigationService, IApiService apiService, IFilesHelper filesHelper) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            _filesHelper = filesHelper;
            UsuarioLogueado = JsonConvert.DeserializeObject<UsuarioAppResponse>(Settings.UsuarioLogueado);
            IsEnabled = false;
            Title = "Inspecciones";

        }
    }
}
