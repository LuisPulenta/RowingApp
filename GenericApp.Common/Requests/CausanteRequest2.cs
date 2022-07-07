using System;
using System.ComponentModel.DataAnnotations;

namespace GenericApp.Common.Requests
{
    public class CausanteRequest2
    {
        [Required]
        public int Id { get; set; }

        public string telefono { get; set; }

        public byte[] Image { get; set; }
        public string direccion { get; set; }
        public int Numero { get; set; }
        public string TelefonoContacto1 { get; set; }
        public string TelefonoContacto2 { get; set; }
        public string TelefonoContacto3 { get; set; }
        public DateTime fecha { get; set; }
        public string NotasCausantes { get; set; }
        public string ciudad { get; set; }
        public string Provincia { get; set; }
    }
}