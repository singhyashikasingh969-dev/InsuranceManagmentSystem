namespace InsuranceManagmentSystem.Models
{
    public class Policy
    {
        public int PolicyID { get; set; }
        public int CustomerID { get; set; }
        public string PolicyType { get; set; } = "";
        public decimal PremiumAmount { get; set; }

        public decimal CoverageAmount { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? AgentID { get; set; }
    }
}