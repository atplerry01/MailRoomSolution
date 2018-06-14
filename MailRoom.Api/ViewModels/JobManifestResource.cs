using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MailRoom.Api.ViewModels
{
    public class JobManifestResource
    {
        public int Id { get; set; }
        public string WayBillNumber { get; set; }
        public string TrackingNumber { get; set; }
        public string JobId { get; set; }
        
        public ICollection<JobManifestBranchResource> JobManifestBranchs { get; set; }
        public ICollection<JobManifestLogResource> JobManifestLogs { get; set; }
        
        public JobManifestResource()
        {
            JobManifestBranchs = new Collection<JobManifestBranchResource>();
            JobManifestLogs = new Collection<JobManifestLogResource>();
        }
    }
}