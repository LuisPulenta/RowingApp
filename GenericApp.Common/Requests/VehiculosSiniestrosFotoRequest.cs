namespace GenericApp.Common.Requests
{
    public class VehiculosSiniestrosFotoRequest
    {
        public int IDFOTOSINIESTRO { get; set; }
        public int NROSINIESTROCAB { get; set; }
        public string OBSERVACION { get; set; }
        public byte[] ImageArray { get; set; }
        public string LINKFOTO { get; set; }
        public string CORRESPONDEA { get; set; }
    }
}
