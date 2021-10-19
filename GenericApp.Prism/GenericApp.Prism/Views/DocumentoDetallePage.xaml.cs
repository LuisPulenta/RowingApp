using System;
using Xamarin.Forms;

namespace GenericApp.Prism.Views
{
    public partial class DocumentoDetallePage : ContentPage
    {
        public DocumentoDetallePage()
        {
            InitializeComponent();
        }

        protected void webOnNavigating(object sender, WebNavigatingEventArgs e)
        {




            if (e.Url.Contains(".pdf"))
            {
                // retornando a URL
                var pdfUrl = new Uri(e.Url);

                // Abra a URL do PSD com o navegador para download
                Device.OpenUri(pdfUrl);

                // Cancela navegacao ao clicar
                // (reêm a mesma pagina.)  
                e.Cancel = true;
            }
        }
    }
}
