using System.ComponentModel.DataAnnotations.Schema;

namespace MailRoom.Api.Models
{
    public class JobManifestLog
    {
        public int Id { get; set; }
        public string SN { get; set; }
        public string Name { get; set; }
        public string Pan { get; set; }
        public string BranchCode { get; set; }
        public string BranchName { get; set; }
        public string CustodianName { get; set; }
        public string CustodianNumber { get; set; }
        public string AccountNumber { get; set; }
        public string FileName { get; set; }
        
        public int JobManifestId { get; set; }
        public int JobManifestBranchId { get; set; }

        // [ForeignKey("JobManifestId")]
        // public JobManifest JobManifest { get; set; }

        [ForeignKey("JobManifestBranchId")]
        public JobManifestBranch JobManifestBranch { get; set; }
        
    }
}