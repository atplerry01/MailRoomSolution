using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MailRoom.Api.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<ClientBranch> Branches { get; set; }
        
        public Client()
        {
            Branches = new Collection<ClientBranch>();
        }
    }
}