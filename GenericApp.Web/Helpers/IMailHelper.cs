using GenericApp.Common.Responses;
using System.Threading.Tasks;

namespace GenericApp.Web.Helpers
{
    public interface IMailHelper
    {
        Response SendMail(string to, string subject, string body);

        Task<Response> SendMailWithPdf(string to, string subject, string body, string fileUrl);
    }
}