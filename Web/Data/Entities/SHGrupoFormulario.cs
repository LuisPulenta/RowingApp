﻿using System.ComponentModel.DataAnnotations;

namespace RowingApp.Web.Data.Entities
{
    public class SHGrupoFormulario
    {
        public int IDCLIENTE { get; set; }
        public int IDTIPOTRABAJO { get; set; }
        public int IDGRUPOFORMULARIO { get; set; }
        public string DESCRIPCION { get; set; }
    }
}