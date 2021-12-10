using GenericApp.Common.Helpers;
using GenericApp.Common.Models;
using GenericApp.Common.Responses;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;

namespace GenericApp.Prism.ViewModels
{
    public class ReclamoItemViewModel : ObrasPosteResponse
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _selectReclamoCommand;

        public ReclamoItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
        public DelegateCommand SelectReclamoCommand => _selectReclamoCommand ?? (_selectReclamoCommand = new DelegateCommand(SelectReclamo));



        private async void SelectReclamo()
        {
            NavigationParameters parameters = new NavigationParameters
            {
                { "obra", this }
            };
            Settings.Obra = JsonConvert.SerializeObject(this);
            await _navigationService.NavigateAsync("ReclamoEditPage", parameters);
        }
    }
}
