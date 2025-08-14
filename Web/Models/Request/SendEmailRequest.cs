namespace GenericApp.Common.Requests
{
    public class SendEmailRequest
    {

        public string to { get; set; }
        public string subject { get; set; }
        public string body { get; set; }
        public string fileUrl { get; set; }
        public string fileName { get; set; }
    }
}