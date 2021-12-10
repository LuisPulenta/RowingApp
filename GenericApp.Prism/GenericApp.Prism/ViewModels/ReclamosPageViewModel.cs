using GenericApp.Common.Helpers;
using GenericApp.Common.Responses;
using GenericApp.Common.Services;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using System;

namespace GenericApp.Prism.ViewModels
{
    public class ReclamosPageViewModel : ViewModelBase
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

        private int _nroReg;
        public int NroReg
        {
            get => _nroReg;
            set => SetProperty(ref _nroReg, value);
        }

        private string _zona;
        public string Zona
        {
            get => _zona;
            set => SetProperty(ref _zona, value);
        }

        private string _descripcion;
        public string Descripcion
        {
            get => _descripcion;
            set => SetProperty(ref _descripcion, value);
        }

        private string _direccion;
        public string Direccion
        {
            get => _direccion;
            set => SetProperty(ref _direccion, value);
        }

        private string _numero;
        public string Numero
        {
            get => _numero;
            set => SetProperty(ref _numero, value);
        }

        private string _nroReclamo;
        public string NroReclamo
        {
            get => _nroReclamo;
            set => SetProperty(ref _nroReclamo, value);
        }

        private DelegateCommand _addReclamo;
        public DelegateCommand AddReclamo => _addReclamo ?? (_addReclamo = new DelegateCommand(AddAsync));

        

        public ReclamosPageViewModel(INavigationService navigationService, IApiService apiService, IFilesHelper filesHelper) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            _filesHelper = filesHelper;
            Obra = JsonConvert.DeserializeObject<ObraResponse>(Settings.Obra);
            NroObra = Obra.NroObra;
            UsuarioLogueado = JsonConvert.DeserializeObject<UsuarioAppResponse>(Settings.UsuarioLogueado);
            IsEnabled = false;
            Title = "Reclamos";
        }

        private async void AddAsync()
        {
            Settings.Obra = JsonConvert.SerializeObject(this);
            await _navigationService.NavigateAsync("ReclamoPage");
        }
    }
}