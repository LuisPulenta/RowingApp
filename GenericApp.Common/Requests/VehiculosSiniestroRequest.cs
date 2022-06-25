using System;

namespace GenericApp.Common.Requests
{
    public class VehiculosSiniestroRequest
    {
        public int NROSINIESTRO { get; set; }
        public DateTime FECHACARGA { get; set; }
        public string GRUPO { get; set; }
        public string CAUSANTE { get; set; }
        public string APELLIDONOMBRETERCERO { get; set; }
        public string NROPOLIZATERCERO { get; set; }
        public string TELEFONOCONTACTOTERCERO { get; set; }
        public string EMAILTERCERO { get; set; }
        public string NOTIFICADOEMPRESA { get; set; }
        public string NOTIFICADOA { get; set; }
        public string DIRECCIONSINIESTRO { get; set; }
        public string ALTURA { get; set; }
        public string CIUDAD { get; set; }
        public string PROVINCIA { get; set; }
        public int HORASINIESTRO { get; set; }
        public string LESIONADOS { get; set; }
        public int CANTIDADLESIONADOS { get; set; }
        public string INTERVINOPOLICIA { get; set; }
        public string INTERVINOAMBULANCIA { get; set; }
        public string RELATOSINIESTRO { get; set; }
        public string NUMCHA { get; set; }
        public string COMPANIASEGUROTERCERO { get; set; }
        public int IDUSUARIOCARGA { get; set; }
    }
}
