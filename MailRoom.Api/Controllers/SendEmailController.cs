using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MailRoom.Api.Controllers
{
    [Route("api/sendmail")]
    public class SendEmailController: Controller
    {
        public SendEmailController()
        {
            
        }

        [HttpGet]
        public async Task<IActionResult> GetJobManifest()
        {
            string text= "<table><tr><td>EmpId</td><td>Emp name</td><td>age</td></tr><tr><td>value</td><td>value</td><td>value</td></tr></table>";

            var mailMessage = new MailMessage
            {
                From = new MailAddress("lakinsanya@secureidltd.com"),
                Subject = "Hello World",
                IsBodyHtml = true,
                Body = text //"Test email from Send Grid SMTP Settings"
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