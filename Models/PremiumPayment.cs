using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsuranceManagmentSystem.Models
{
    [Table("PremiumPayment")]
   
    public class PremiumPayment
    {
        [Key]
        public int PaymentID { get; set; }

        public int PolicyID { get; set; }

        public decimal Amount { get; set; }

        public DateTime PaymentDate { get; set; }

        public string PaymentStatus { get; set; } = "";
    }
}