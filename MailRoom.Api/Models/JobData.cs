using System.ComponentModel.DataAnnotations.Schema;

namespace MailRoom.Api.Models
{
    public class JobData
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

        public string JobId { get; set; }
        
        // public int ClientBranchId { get; set; }

        // [ForeignKey("ClientBranchId")]
        // public ClientBranch ClientBranch { get; set; }

    }
}