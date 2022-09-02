namespace GenericApp.Common.Responses
{
    public class UsuarioAppResponse
    {
        public int IDUsuario { get; set; }
        public string CodigoCausante { get; set; }
        public string Login { get; set; }
        public string Contrasena { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int? AutorWOM { get; set; }
        public byte Estado { get; set; }
        public int? HabilitaAPP { get; set; }
        public int? HabilitaFotos { get; set; }
        public int? HabilitaReclamos { get; set; }
        public int? HabilitaSSHH { get; set; }
        public int? HabilitaRRHH { get; set; }
        public string Modulo { get; set; }
        public int? HabilitaMedidores { get; set; }
        public string HabilitaFlotas { get; set; }
        public int? ReHabilitaUsuarios { get; set; }
        public string CODIGOGRUPO { get; set; }
        public string FullName => $"{Nombre} {Apellido}";
        public int? FechaCaduca { get; set; }
        public int? IntentosInvDiario { get; set; }
        public int? OpeAutorizo { get; set; }
    }
}