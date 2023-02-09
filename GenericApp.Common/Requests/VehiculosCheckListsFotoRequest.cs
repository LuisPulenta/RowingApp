using GenericApp.Common.Responses;
using System;

namespace GenericApp.Common.Requests
{
    public class VehiculosCheckListsFotoRequest
    {

        public int IDREGISTRO { get; set; }
        public int IDCHECKLISTCAB { get; set; }
        public string DESCRIPCION { get; set; }
        public byte[] ImageArray { get; set; }
    }
}
