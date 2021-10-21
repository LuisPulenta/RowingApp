using GenericApp.Prism.ViewModels;
using Xamarin.Forms;

namespace GenericApp.Prism.Views
{
    public partial class Entregas2Page : ContentPage
    {
        public Entregas2Page()
        {
            InitializeComponent();
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var entregas2PageViewModel = Entregas2PageViewModel.GetInstance();
            entregas2PageViewModel.RefreshList();
        }
    }
}
