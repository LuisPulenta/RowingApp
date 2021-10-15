using GenericApp.Common.Helpers;
using GenericApp.Common.Responses;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;

namespace GenericApp.Prism.ItemViewModels
{
    public class DocumentoItemViewModel : ObraDocumentoResponse
    {
        private readonly INavigationService _navigationService;

        private DelegateCommand _selectDocumentoCommand;
        public DelegateCommand SelectDocumentoCommand => _selectDocumentoCommand ?? (_selectDocumentoCommand = new DelegateCommand(SelectDocumento));

        public DocumentoItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }



        private async void SelectDocumento()
        {
            Settings.Documento = JsonConvert.SerializeObject(this);
            await _navigationService.NavigateAsync("DocumentoDetallePage");
        }
    }
}