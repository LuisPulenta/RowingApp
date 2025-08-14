using System;
using System.ComponentModel.DataAnnotations;

namespace RowingApp.Web.Data.Entities
{
    public class SHInspeccionDetall
    {
        [Key]
        public int IDRegistro { get; set; }
        public int InspeccionCab { get; set; }
        public int IdCliente { get; set; }
        public int IDGrupoFormulario { get; set; }
        public string DetalleF { get; set; }
        public string Descripcion { get; set; }
        public int PonderacionPuntos { get; set; }
        public string Cumple { get; set; }
        public string LinkFoto { get; set; }
        public string ObsAPP { get; set; }
        public int? SoloTexto { get; set; }

        public string ImageFullPath => string.IsNullOrEmpty(LinkFoto)
        ? $"http://190.111.249.225/RowingAppApi/images/Inspecciones/noimage.png"
        : $"http://190.111.249.225/RowingAppApi{LinkFoto.Substring(1)}";


    }
}