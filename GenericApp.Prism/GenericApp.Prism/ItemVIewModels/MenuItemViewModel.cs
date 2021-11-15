using GenericApp.Common.Helpers;
using GenericApp.Common.Models;
using GenericApp.Common.Responses;
using GenericApp.Prism.Views;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using System;

namespace GenericApp.Prism.ItemViewModels
{
    public class MenuItemViewModel : Menu
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _selectMenuCommand;


        public UsuarioAppResponse UsuarioLogueado;

        public DateTime FechaLogueado;

        public MenuItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            UsuarioLogueado = JsonConvert.DeserializeObject<UsuarioAppResponse>(Settings.UsuarioLogueado);
            FechaLogueado = JsonConvert.DeserializeObject<DateTime>(Settings.FechaLogueado);
        }

        public DelegateCommand SelectMenuCommand => _selectMenuCommand ?? (_selectMenuCommand = new DelegateCommand(SelectMenuAsync));

        private async void SelectMenuAsync()
        {
            if (FechaLogueado<DateTime.Today)
            {
                await App.Current.MainPage.DisplayAlert("Aviso", "Debe loguearse al menos una vez en el día", "Aceptar");
                await _navigationService.NavigateAsync($"{nameof(LoginPage)}");
                return;
            }


            if (PageName == nameof(LoginPage) && Settings.IsLogin)
            {
                Settings.IsLogin = false;
                Settings.Token = null;
            }

            if (IsLoginRequired && !Settings.IsLogin)
            {
                //await App.Current.MainPage.DisplayAlert("Error", "Debe estar logueado", "Aceptar");
                NavigationParameters parameters = new NavigationParameters
                    {
                        { "pageReturn", PageName }
                    };

                //await _navigationService.NavigateAsync($"/{nameof(GenericAppMasterDetailPage)}/NavigationPage/{nameof(LoginPage)}", parameters);
                await _navigationService.NavigateAsync($"{nameof(LoginPage)}");
            }
            else
            {
                if (PageName == "SegHigPage" && UsuarioLogueado.HabilitaSSHH != 1)
                {
                    await App.Current.MainPage.DisplayAlert("Aviso!", "Su Usuario no está habilitado para esta opción.", "Aceptar");
                    return;
                }

                if (PageName == "MedidoresPage" && UsuarioLogueado.HabilitaMedidores != 1)
                {
                    await App.Current.MainPage.DisplayAlert("Aviso!", "Su Usuario no está habilitado para esta opción.", "Aceptar");
                    return;
                }

                if (PageName == "LoginPage")
                {
                    await _navigationService.NavigateAsync($"{nameof(LoginPage)}");
                    return;
                }

                await _navigationService.NavigateAsync($"/{nameof(GenericAppMasterDetailPage)}/NavigationPage/{PageName}");

            }

            
        }
    }
}