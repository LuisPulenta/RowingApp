using System.ComponentModel.DataAnnotations;

namespace GenericApp.Common.Requests
{
    public class UsuarioFirmaRequest
    {
        [Required]
        public int IDUsuario { get; set; }
        public byte[] ImageArrayFirmaUsuario { get; set; }
    }
}