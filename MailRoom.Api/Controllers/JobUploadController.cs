using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MailRoom.Api.Models;
using MailRoom.Api.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MailRoom.Api.Controllers
{
    [Route("api/jobupload")]
    public class JobUploadController : Controller
    {
        private readonly IHostingEnvironment host;
        private readonly ApplicationDbContext context;
        public JobUploadController(ApplicationDbContext context, IHostingEnvironment host)
        {
            this.context = context;
            this.host = host;
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file) //IFormFile file, IFormCollection files
        {
            var uploadFolderPath = Path.Combine(host.WebRootPath, "uploads");
            if (Directory.Exists(uploadFolderPath))
                Directory.CreateDirectory(uploadFolderPath);

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadFolderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Process the uploaded file
            //Read the contents of CSV file.
            string csvData = System.IO.File.ReadAllText(filePath);

            //Execute a loop over the rows.
            foreach (string row in csvData.Split('\n'))
            {
                if (!string.IsNullOrEmpty(row))
                {

                    // get brachId
                    var brCode = row.Split(';')[0];

                    var branch = context.ClientBranches.FirstOrDefault(b => b.BranchCode == brCode);

                    JobData jobData = new JobData()
                    {
                        ClientBranchId = branch.Id
                    };
                    var col1 = row.Split(';')[0];
                    var col2 = row.Split(';')[1];
                    var col3 = row.Split(';')[2];

                    //Build the object data

                }
            }



            // //Todo: Creating a thumnail for the file
            // var photo = new IssueImage { FileName = fileName };
            // issueTracker.IssueImages.Add(photo);
            // await context.SaveChangesAsync();

            // return Ok(mapper.Map<IssueImage, IssueImageResource>(photo));
            return Ok();
        }

    }
}