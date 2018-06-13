using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MimeKit;

namespace MailRoom.Api.Controllers
{
    [Route("api/sendmail")]
    public class SendEmailController : Controller
    {
        private readonly IHostingEnvironment host;
        public SendEmailController(IHostingEnvironment host)
        {
            this.host = host;
        }

        [HttpGet]
        public async Task<IActionResult> GetJobManifest()
        {

            var uploadFolderPath = Path.Combine(host.WebRootPath, "templates/email-template");
            if (Directory.Exists(uploadFolderPath))
                Directory.CreateDirectory(uploadFolderPath);

            var fileName = "Confirm_Account_Registration.html"; //Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadFolderPath, fileName);

            var builder = new BodyBuilder();

            using (StreamReader SourceReader = System.IO.File.OpenText(filePath))
            {
                builder.HtmlBody = SourceReader.ReadToEnd();
            }

            var subject = "Subject";
            var Email = "atplerry@gmail.com";
            var Password = "olami";
            var Message = "Messaage End";
            var callbackUrl = "http://google.com";

            string messageBody = string.Format(builder.HtmlBody,
                        subject,
                        String.Format("{0:dddd, d MMMM yyyy}", DateTime.Now),
                        Email,
                        Email,
                        Password,
                        Message,
                        callbackUrl
                        );

            /////////////////////////
            string text = "<table><tr><td>EmpId</td><td>Emp name</td><td>age</td></tr><tr><td>value</td><td>value</td><td>value</td></tr></table>";

            var mailMessage = new MailMessage
            {
                From = new MailAddress("lakinsanya@secureidltd.com"),
                Subject = "Hello World",
                IsBodyHtml = true,
                Body = messageBody //text //"Test email from Send Grid SMTP Settings"
            };

            mailMessage.To.Add("atplerry@gmail.com, lakinsanya@secureidltd.com");

            var smtpClient = new SmtpClient
            {
                Credentials = new NetworkCredential("lakinsanya@secureidltd.com", "Whycespace@01"),
                Host = "smtp.secureidltd.com", //smtp.sendgrid.net",
                Port = 587
            };

            smtpClient.Send(mailMessage);

            return Ok();
        }

    }
}