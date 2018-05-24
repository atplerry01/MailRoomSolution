using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace MailRoom.Api.Models
{
    public class JobManifestBranch
    {
        public int Id { get; set; }
        public int DataQuantity { get; set; }
        public int ClientBranchId { get; set; }
        public int JobManifestId { get; set; }
        

        public ClientBranch ClientBranch { get; set; }
        public JobManifest JobManifest { get; set; }


        public ICollection<JobManifestLog> JobManifestLogs { get; set; }
        public JobManifestBranch()
        {
            JobManifestLogs = new Collection<JobManifestLog>();
        }

    }
}