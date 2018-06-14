using System.Collections.Generic;
using System.Collections.ObjectModel;
using MailRoom.Api.Models;

namespace MailRoom.Api.ViewModels
{
    public class JobManifestBranchResource
    {
        public int Id { get; set; }
        public string JobId { get; set; }
        public int DataQuantity { get; set; }
        public string WayBillNumber { get; set; }
        public string TrackingNumber { get; set; }
        public string GraphicImage { get; set; }
        public ClientBranchResource ClientBranch { get; set; }
        public ICollection<JobManifestLogResource> JobManifestLogs { get; set; }
        public JobManifestBranchResource()
        {
            JobManifestLogs = new Collection<JobManifestLogResource>();
        }

    }
}