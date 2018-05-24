using System.ComponentModel.DataAnnotations.Schema;

namespace MailRoom.Api.Models
{
    public class CardCustodian
    {
        public int Id { get; set; }
        public string PrimaryCustodian { get; set; }
        public string SecondaryCustodian { get; set; }
        //public int ClientHeadQuarterId { get; set; }


        // [ForeignKey("ClientHeadQuarterId")]
        // public ClientHeadQuarter ClientHeadQuarter { get; set; }
        public int ClientBranchId { get; set; }
        [ForeignKey("ClientBranchId")]
        public ClientBranch ClientBranch { get; set; }

    }
}