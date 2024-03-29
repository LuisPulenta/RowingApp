﻿using GenericApp.Common.Helpers;
using GenericApp.Common.Responses;
using Newtonsoft.Json;
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
            Settings.Entrega = JsonConvert.SerializeObject(this);
            await _navigationService.NavigateAsync("EntregaDetallePage");
        }
    }
}