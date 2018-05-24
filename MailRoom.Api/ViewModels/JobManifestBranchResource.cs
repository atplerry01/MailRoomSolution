using System.Collections.Generic;
using System.Collections.ObjectModel;
using MailRoom.Api.Models;

namespace MailRoom.Api.ViewModels
{
    public class JobManifestBranchResource
    {
        public int Id { get; set; }
        public int DataQuantity { get; set; }
        public ICollection<JobManifestLogResource> JobManifestLogs { get; set; }
        public JobManifestBranchResource()
        {
            JobManifestLogs = new Collection<JobManifestLogResource>();
        }

    }
}