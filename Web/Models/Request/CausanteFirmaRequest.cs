using System.ComponentModel.DataAnnotations;

namespace RowingApp.Common.Requests
{
    public class CausanteFirmaRequest
    {
        [Required]
        public int NroCausante { get; set; }
        public byte[] ImageArrayFirmaUsuario { get; set; }
    }
}