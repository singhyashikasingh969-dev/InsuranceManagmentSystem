using System;

namespace InsuranceManagmentSystem.Models
{
    public class CompanyPremiumViewModel
    {
        public string CompanyName { get; set; } = "";

        public string AgentName { get; set; } = "";

        public int PolicyID { get; set; }

        public int PaymentID { get; set; }

        public decimal Amount { get; set; }

        public DateTime PaymentDate { get; set; }

        public string PaymentStatus { get; set; } = "";
    }
}