using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MailRoom.Api.Models;
using MailRoom.Api.Persistence;
using MailRoom.Api.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MailRoom.Api.Controllers
{
    [Route("api/jobmanifests")]
    public class JobManifestController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        public JobManifestController(ApplicationDbContext context, IMapper mapper)
        {
            this.mapper = mapper;
            this.context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<JobManifestResource>> GetIssueTrackers()
        {
            var jobManifests = await context.JobManifests
                .Include(m => m.JobManifestBranchs)
                .Include(l => l.JobManifestLogs)
                .ToListAsync();
            return mapper.Map<IEnumerable<JobManifest>, IEnumerable<JobManifestResource>>(jobManifests);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetIssueTracker(int id)
        {
            var jobManifest = await context.JobManifests
                 .Include(m => m.JobManifestBranchs)
                .Include(l => l.JobManifestLogs)
                .SingleOrDefaultAsync(it => it.Id == id);

            if (jobManifest == null) return NotFound();

            var jobManifestResource = mapper.Map<JobManifest, JobManifestResource>(jobManifest);

            return Ok(jobManifestResource);
        }

    }
}