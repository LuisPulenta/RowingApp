using System.ComponentModel.DataAnnotations;

namespace GenericApp.Common.Requests
{
    public class CausanteFirmaRequest
    {
        [Required]
        public int NroCausante { get; set; }
        public byte[] ImageArrayFirmaUsuario { get; set; }
    }
}