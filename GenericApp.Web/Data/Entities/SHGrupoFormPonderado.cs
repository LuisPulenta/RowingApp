using System.ComponentModel.DataAnnotations;

namespace GenericApp.Web.Data.Entities
{
    public class SHGrupoFormPonderado
    {
        public int IDCLIENTE { get; set; }
        public int IDGRUPOFORMULARIO { get; set; }
        public string DETALLEF { get; set; }
        public string DESCRIPCION { get; set; }
        public int PONDERACIONPUNTOS { get; set; }
        public string CUMPLE { get; set; }

    }
}