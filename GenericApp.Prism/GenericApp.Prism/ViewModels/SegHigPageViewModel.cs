using GenericApp.Common.Responses;
using GenericApp.Common.Services;
using Prism.Navigation;


namespace GenericApp.Prism.ViewModels
{
    public class SegHigPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private UsuarioAppResponse _user;
        public UsuarioAppResponse User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }

        public string Legajo { get; set; }

        public SegHigPageViewModel(INavigationService navigationService, IApiService apiService) : base(navigationService)
        {
            _apiService = apiService;
            _navigationService = navigationService;
            Title = "Seguridad e Higiene";
        }
    }
}
