using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using AutoMapper;
using MailRoom.Api.Models;
using MailRoom.Api.Persistence;
using MailRoom.Api.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace MailRoom.Api.Controllers
{
    [Route("api/jobupload")]
    public class JobUploadController : Controller
    {
        private readonly IHostingEnvironment host;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly HttpClient client;
        public JobUploadController(ApplicationDbContext context, IMapper mapper, IHostingEnvironment host, HttpClient client)
        {
            this.client = client;
            this.mapper = mapper;
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

            // Process JobData
            #region ProcessJobData

            //Execute a loop over the rows.
            foreach (string row in csvData.Split('\n'))
            {
                if (!string.IsNullOrEmpty(row))
                {
                    // get branchId
                    var sn = row.Split(',')[0];
                    var name = row.Split(',')[1];
                    var pan = row.Split(',')[2];
                    var brCode = row.Split(',')[3];
                    var brName = row.Split(',')[4];
                    var customerName = row.Split(',')[5];
                    var customerNumber = row.Split(',')[6];
                    var accNumber = row.Split(',')[7];
                    var fName = row.Split(',')[8];

                    JobData jobData = new JobData()
                    {
                        SN = sn,
                        Name = name,
                        Pan = pan,
                        BranchCode = brCode,
                        BranchName = brName,
                        CustodianName = customerName,
                        CustodianNumber = customerNumber,
                        AccountNumber = accNumber,
                        FileName = fName,
                        JobId = file.FileName
                    };

                    context.Jobdatas.Add(jobData);
                }
            }

            await context.SaveChangesAsync();

            #endregion

            //Process the JobManifest Data
            #region ProcessJobManifest

            JobManifest jobManifest = new JobManifest()
            {
                WayBillNumber = fileName,
                //TrackingNumber = Guid.NewGuid().ToString(),
                JobId = file.FileName
            };

            context.JobManifests.Add(jobManifest);
            await context.SaveChangesAsync();

            // get distinct BranchCode
            var selectedBranchCode = context.Jobdatas.Select(b => b.BranchCode).Distinct();

            // Iterate thru the branchCode
            foreach (string branch in selectedBranchCode)
            {
                //get the count
                var branchData = context.Jobdatas.Where(b => b.BranchCode == branch);
                var clientBranch = context.ClientBranches.SingleOrDefault(c => c.BranchCode == branch);
                var dataCount = branchData.Count();

                if (clientBranch != null)
                {
                    //Create Branch Manifest
                    JobManifestBranch branchManifest = new JobManifestBranch()
                    {
                        DataQuantity = dataCount,
                        JobManifestId = jobManifest.Id,
                        ClientBranchId = clientBranch.Id,
                        JobId = file.FileName
                    };

                    context.JobManifestBranches.Add(branchManifest);
                    await context.SaveChangesAsync();


                    // Create the Branch Logs
                    foreach (var data in branchData)
                    {
                        JobManifestLog manifestLog = new JobManifestLog()
                        {
                            SN = data.SN,
                            Name = data.Name,
                            Pan = data.Pan,
                            BranchCode = data.BranchCode,
                            BranchName = data.BranchName,
                            CustodianName = data.CustodianName,
                            CustodianNumber = data.CustodianNumber,
                            AccountNumber = data.AccountNumber,
                            FileName = data.FileName,

                            JobManifestId = jobManifest.Id,
                            JobManifestBranchId = branchManifest.Id
                        };

                        context.JobManifestLogs.Add(manifestLog);
                    }

                    await context.SaveChangesAsync();
                }
            }

            #endregion

            // Delete the JobData
            #region DeleteJobData

            var fileJobDatas = context.Jobdatas.Where(j => j.JobId == file.FileName);

            foreach (var job in fileJobDatas)
            {
                context.Jobdatas.Remove(job);
            }

            await context.SaveChangesAsync();

            #endregion


            //Geth the manifest for the doc
            var completedManifest = await context.JobManifests
                .Include(m => m.JobManifestBranchs)
                .Include(l => l.JobManifestLogs)
                .SingleOrDefaultAsync(it => it.Id == jobManifest.Id);

            if (jobManifest == null) return NotFound();

            var jobManifestResource = mapper.Map<JobManifest, JobManifestResource>(completedManifest);

            return Ok(jobManifestResource);
        }

    }
}