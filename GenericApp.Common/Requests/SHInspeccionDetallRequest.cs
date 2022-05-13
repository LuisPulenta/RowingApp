using System;
using System.Collections.Generic;
using System.Text;

namespace GenericApp.Common.Requests
{
    public class SHInspeccionDetallRequest
    {
        public int IDRegistro { get; set; }
        public int InspeccionCab { get; set; }
        public int IdCliente { get; set; }
        public int IDGrupoFormulario { get; set; }
        public string DetalleF { get; set; }
        public string Descripcion { get; set; }
        public int PonderacionPuntos { get; set; }
        public string Cumple { get; set; }
        public string LinkFoto { get; set; }
        public byte[] ImageArray { get; set; }
    }
}
