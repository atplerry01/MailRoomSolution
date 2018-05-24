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
                if (!string.IsNullOrEmpty(row) && row.Split(',')[0] != "SN")
                {
                    // get brachId
                    var sn = row.Split(',')[0];
                    var name = row.Split(',')[1];
                    var pan = row.Split(',')[2];
                    var brCode = row.Split(',')[3];
                    var brName = row.Split(',')[4];
                    var custName = row.Split(',')[5];
                    var custNumber= row.Split(',')[6];
                    var accNumber = row.Split(',')[7];
                    var fName = row.Split(',')[8];

                    //var branch = context.ClientBranches.FirstOrDefault(b => b.BranchCode == brCode);

                    JobData jobData = new JobData()
                    {
                        SN = sn,
                        Name = name,
                        Pan = pan,
                        BranchCode = brCode,
                        BranchName = brName,
                        CustodianName = custName,
                        CustodianNumber = custNumber,
                        AccountNumber = accNumber,
                        FileName = fName
                    };

                    context.Jobdatas.Add(jobData);

                }
            }

            await context.SaveChangesAsync();

            //Process the Database Data
            // get distinct BranchCode
            var selectedBranchCode = context.Jobdatas.Select(b => b.BranchCode).Distinct();
            var test = "";
            return Ok();
        }

    }
}