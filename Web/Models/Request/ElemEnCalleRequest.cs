using System;

namespace RowingApp.Common.Requests
{
    public class ElemEnCalleRequest
    {
        public int ID { get; set; }
        public int NROOBRA { get; set; }
        public int IDUSERCARGA { get; set; }
        public string GRXX { get; set; }
        public string GRYY { get; set; }
        public string DOMICILIO { get; set; }
        public string OBSERVACION { get; set; }
        public string ESTADO { get; set; }
        public int? IDUSERRECUPERA { get; set; }
        public byte[] ImageArray { get; set; }
    }
}
