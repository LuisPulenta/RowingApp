using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using RowingApp.Common.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;

namespace RowingApp.Web.Helpers
{
    public class MailHelper : IMailHelper
    {
        private readonly IConfiguration _configuration;

        public MailHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Response SendMail(string to, string subject, string body)
        {
            try
            {
                string from = _configuration["Mail:From"];
                string smtp = _configuration["Mail:Smtp"];
                string port = _configuration["Mail:Port"];
                string password = _configuration["Mail:Password"];

                List<string> listaTO = to.Split(',').ToList();

                MimeMessage message = new MimeMessage();
                message.From.Add( MailboxAddress.Parse(from));
                foreach (var email in listaTO)
                {
                    message.To.Add( MailboxAddress.Parse(email));
                }

                message.Subject = subject;

                BodyBuilder bodyBuilder = new BodyBuilder
                {
                    HtmlBody = body

                };
                message.Body = bodyBuilder.ToMessageBody();

                using (SmtpClient client = new SmtpClient())
                {
                    client.Connect(smtp, int.Parse(port), true);
                    client.Authenticate(from, password);
                    client.Send(message);
                    client.Disconnect(true);
                }

                return new Response { IsSuccess = true };

            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Result = ex
                };
            }
        }

        public async Task<Response> SendMailWithPdf (string to, string subject, string body, string fileUrl, string fileName)
        {
            try
            {
                string from = _configuration["Mail:From"];
                string smtp = _configuration["Mail:Smtp"];
                string port = _configuration["Mail:Port"];
                string password = _configuration["Mail:Password"];

                List<string> listaTO = to.Split(',').ToList();

                MimeMessage message = new MimeMessage();
                message.From.Add( MailboxAddress.Parse(from));
                foreach (var email in listaTO)
                {
                    message.To.Add( MailboxAddress.Parse(email));
                }

                message.Subject = subject;

                BodyBuilder bodyBuilder = new BodyBuilder
                {
                    HtmlBody = body

                };

                if (!string.IsNullOrEmpty(fileUrl))
                {
                    using (HttpClient client = new HttpClient())
                    {
                        var fileBytes = await client.GetByteArrayAsync(fileUrl);
                        //var fileName = Path.GetFileName(fileUrl);

                        // Crear un ContentType para el archivo PDF
                        var contentType = new MimeKit.ContentType("application", "pdf");

                        // Agregar el archivo PDF como adjunto
                        bodyBuilder.Attachments.Add(fileName, fileBytes, contentType);
                    }
                }

                message.Body = bodyBuilder.ToMessageBody();

                using (SmtpClient client = new SmtpClient())
                {
                    client.Connect(smtp, int.Parse(port), true);
                    client.Authenticate(from, password);
                    client.Send(message);
                    client.Disconnect(true);
                }

                return new Response { IsSuccess = true };

            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Result = ex
                };
            }
        }
    }
}