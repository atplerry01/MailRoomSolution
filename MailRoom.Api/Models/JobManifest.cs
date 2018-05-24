using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MailRoom.Api.Models
{
    public class JobManifest
    {
        public int Id { get; set; }
        public string WayBillNumber { get; set; }
        public string BranchCode { get; set; }
        
        public ICollection<JobBranchManifest> JobBranchManifests { get; set; }
        
        public JobManifest()
        {
            JobBranchManifests = new Collection<JobBranchManifest>();
        }
        
    }
}