﻿using System.ComponentModel.DataAnnotations;

namespace GenericApp.Web.Data.Entities
{
    public class Parametro
    {
        [Key]
        public int ID { get; set; }
        public int? BLOQUEAACTAS { get; set; }
        public string IPServ { get; set; }
        public int Metros { get; set; }
        public int Tiempo { get; set; }
    }        
}
