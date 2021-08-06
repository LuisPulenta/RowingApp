using GenericApp.Common.Helpers;
using GenericApp.Common.Models;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;

namespace GenericApp.Prism.ViewModels
{
    public class ObraItemViewModel:Obra
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _selectObraCommand;
        
        public ObraItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
        public DelegateCommand SelectObraCommand => _selectObraCommand ?? (_selectObraCommand = new DelegateCommand(SelectObra));



        private async void SelectObra()
        {
            Settings.Obra = JsonConvert.SerializeObject(this);
            await _navigationService.NavigateAsync("ObraPage");
        }
    }
}