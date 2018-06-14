using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MailRoom.Api.Models
{
    public class JobManifest
    {
        public int Id { get; set; }
        public string WayBillNumber { get; set; }
        public string TrackingNumber { get; set; }
        public string GraphicImage { get; set; }
        public string JobId { get; set; }
        
        public ICollection<JobManifestBranch> JobManifestBranchs { get; set; }
        public ICollection<JobManifestLog> JobManifestLogs { get; set; }
        
        public JobManifest()
        {
            JobManifestBranchs = new Collection<JobManifestBranch>();
            JobManifestLogs = new Collection<JobManifestLog>();
        }
        
    }
}