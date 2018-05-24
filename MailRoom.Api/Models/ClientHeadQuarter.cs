using System.ComponentModel.DataAnnotations.Schema;

namespace MailRoom.Api.Models
{
    public class ClientHeadQuarter
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string Address { get; set; }

        [ForeignKey("ClientId")]
        public Client Client { get; set; }

    }
}