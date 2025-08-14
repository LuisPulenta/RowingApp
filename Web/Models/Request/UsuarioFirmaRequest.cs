using System.ComponentModel.DataAnnotations;

namespace RowingApp.Common.Requests
{
    public class UsuarioFirmaRequest
    {
        [Required]
        public int IDUsuario { get; set; }
        public byte[] ImageArrayFirmaUsuario { get; set; }
    }
}