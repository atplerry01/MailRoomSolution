using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace MailRoom.Api.Models
{
    public class JobManifestBranch
    {
        public int Id { get; set; }
        public int JobManifestId { get; set; }

        [ForeignKey("JobManifestId")]
        public JobManifest JobManifest { get; set; }


        public ICollection<JobManifestLog> JobManifestLogs { get; set; }
        public JobManifestBranch()
        {
            JobManifestLogs = new Collection<JobManifestLog>();
        }

    }
}