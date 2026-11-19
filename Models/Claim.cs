namespace InsuranceManagmentSystem.Models
{
    public class Claim
    {
        public int ClaimID { get; set; }
        public int PolicyID { get; set; }
        public DateTime ClaimDate { get; set; }
        public decimal ClaimAmount { get; set; }
        public string ClaimReason { get; set; } = " ";
        public string ClaimStatus { get; set; } = " ";
    }
}
