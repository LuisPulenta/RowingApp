using GenericApp.Prism.ViewModels;
using Syncfusion.SfRotator.XForms;
using System;
using Xamarin.Forms;


namespace GenericApp.Prism.Views
{
    public partial class MedidoresPage : ContentPage
    {
        public MedidoresPage()
        {
            InitializeComponent();
        }

        private void Rotator_SelectedIndexChanged(object sender, SelectedIndexChangedEventArgs e)
        {
            MedidoresPageViewModel medidoresPageViewModel = MedidoresPageViewModel.GetInstance();
            int foto = rotator.SelectedIndex;
            medidoresPageViewModel.IdPhotoNro = foto;
            medidoresPageViewModel.IdPhoto = medidoresPageViewModel.Images[foto].NROREGISTRO;
            //DisplayAlert("Notification", "Selected Index is Changed", "Ok");
        }
    }
}


