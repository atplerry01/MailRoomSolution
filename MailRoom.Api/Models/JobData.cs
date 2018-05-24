using System.ComponentModel.DataAnnotations.Schema;

namespace MailRoom.Api.Models
{
    public class JobData
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int ClientBranchId { get; set; }

        [ForeignKey("ClientBranchId")]
        public ClientBranch ClientBranch { get; set; }

    }
}