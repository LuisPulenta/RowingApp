using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace GenericApp.Common.Helpers
{
    public static class Settings
    {
        private const string _token = "token";
        private const string _isLogin = "isLogin";
        private const string _product = "product";
        private const string _obra = "obra";
        private const string _obrasDocumento = "obrasDocumento";
        private const string _usuarioLogueado = "usuarioLogueado";
        private const string _fechaLogueado = "fechaLogueado";
        private const string _causante= "causante";
        private const string _obrasPoste = "obrasPoste";
        private const string _entrega = "entrega";
        private const string _documento = "documento";
        private static readonly string _stringDefault = string.Empty;
        private static readonly bool _boolDefault = false;

        private static ISettings AppSettings => CrossSettings.Current;

        public static string Token
        {
            get => AppSettings.GetValueOrDefault(_token, _stringDefault);
            set => AppSettings.AddOrUpdateValue(_token, value);
        }

        public static bool IsLogin
        {
            get => AppSettings.GetValueOrDefault(_isLogin, _boolDefault);
            set => AppSettings.AddOrUpdateValue(_isLogin, value);
        }

        public static string Product
        {
            get => AppSettings.GetValueOrDefault(_product, _stringDefault);
            set => AppSettings.AddOrUpdateValue(_product, value);
        }

        public static string UsuarioLogueado
        {
            get => AppSettings.GetValueOrDefault(_usuarioLogueado, _stringDefault);
            set => AppSettings.AddOrUpdateValue(_usuarioLogueado, value);
        }

        public static string FechaLogueado
        {
            get => AppSettings.GetValueOrDefault(_fechaLogueado, _stringDefault);
            set => AppSettings.AddOrUpdateValue(_fechaLogueado, value);
        }

        public static string Obra
        {
            get => AppSettings.GetValueOrDefault(_obra, _stringDefault);
            set => AppSettings.AddOrUpdateValue(_obra, value);
        }
        
        public static string ObrasDocumento
        {
            get => AppSettings.GetValueOrDefault(_obrasDocumento, _stringDefault);
            set => AppSettings.AddOrUpdateValue(_obrasDocumento, value);
        }

        public static string Causante
        {
            get => AppSettings.GetValueOrDefault(_causante, _stringDefault);
            set => AppSettings.AddOrUpdateValue(_causante, value);
        }

        public static string ObrasPoste
        {
            get => AppSettings.GetValueOrDefault(_obrasPoste, _stringDefault);
            set => AppSettings.AddOrUpdateValue(_obrasPoste, value);
        }

        public static string Entrega
        {
            get => AppSettings.GetValueOrDefault(_entrega, _stringDefault);
            set => AppSettings.AddOrUpdateValue(_entrega, value);
        }

        public static string Documento
        {
            get => AppSettings.GetValueOrDefault(_documento, _stringDefault);
            set => AppSettings.AddOrUpdateValue(_documento, value);
        }
    }
}