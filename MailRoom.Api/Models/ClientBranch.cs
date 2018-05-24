using System.ComponentModel.DataAnnotations.Schema;

namespace MailRoom.Api.Models
{
    public class ClientBranch
    {
        public int Id { get; set; }
        public string BranchCode { get; set; }
        public string Address { get; set; }

        public int ClientId { get; set; }

        [ForeignKey("ClientId")]
        public Client Client { get; set; }
    }
}