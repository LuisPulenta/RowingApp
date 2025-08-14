using System;
using System.ComponentModel.DataAnnotations;

namespace RowingApp.Common.Requests
{
    public class ObraEstadoSubestadoRequest
    {
        [Required]
        public int NroObra { get; set; }
        public String CodigoEstado { get; set; }
        public String CodigoSubEstado { get; set; }
    }
}