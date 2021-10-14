using GenericApp.Common.Responses;
using Prism.Commands;
using Prism.Navigation;

namespace GenericApp.Prism.ItemVIewModels
{
    public class EntregaItemViewModel:EntregaResponse
    {
        private readonly INavigationService _navigationService;
        
        private DelegateCommand _selectEntregaCommand;
        public DelegateCommand SelectEntregaCommand => _selectEntregaCommand ?? (_selectEntregaCommand = new DelegateCommand(SelectEntrega));

        public EntregaItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
        
        

        private async void SelectEntrega()
        {
            //Settings.Remote = JsonConvert.SerializeObject(this);
            //await _navigationService.NavigateAsync("RemotePage");
        }
    }
}