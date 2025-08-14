using System.ComponentModel.DataAnnotations;

namespace RowingApp.Web.Data.Entities
{
    public class Grupo
    {
        [Key]
        public int NroGrupo { get; set; }
        public string codigo { get; set; }
        public string detalle { get; set; }
        public int? VisualizaAPP { get; set; }
        public bool Habilitado { get; set; }
        public int? VisualizaSPR { get; set; }
        public int? HabilitaRRHH { get; set; }
    }
}
