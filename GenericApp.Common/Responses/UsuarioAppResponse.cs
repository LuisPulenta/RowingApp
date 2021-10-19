namespace GenericApp.Common.Responses
{
    public class UsuarioAppResponse
    {
        public int IDUsuario { get; set; }
        public string Login { get; set; }
        public string Contrasena { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int? AutorWOM { get; set; }
        public byte Estado { get; set; }
        public int? HabilitaAPP { get; set; }
        public int? HabilitaFotos { get; set; }
        public int? HabilitaReclamos { get; set; }
        public int? HabilitaRRHH { get; set; }
        public int? HabilitaSSHH { get; set; }
        public string Modulo { get; set; }

        public string FullName => $"{Nombre} {Apellido}";
    }
}