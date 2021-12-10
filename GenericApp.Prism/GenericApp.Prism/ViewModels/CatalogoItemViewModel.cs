using GenericApp.Common.Helpers;
using GenericApp.Common.Models;
using GenericApp.Common.Responses;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;

namespace GenericApp.Prism.ViewModels
{
    public class CatalogoItemViewModel : CatalogoResponse
    {
        private readonly INavigationService _navigationService;
       

        public CatalogoItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        private DelegateCommand _selectCatalogoCommand;
        public DelegateCommand SelectCatalogoCommand => _selectCatalogoCommand ?? (_selectCatalogoCommand = new DelegateCommand(SelectCatalogo));



        private async void SelectCatalogo()
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