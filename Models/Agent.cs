using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsuranceManagmentSystem.Models
{
    [Table("Agent")]
    public class Agent
    {
        [Key]
        public int AgentID { get; set; }

        [Required]
        public string AgentName { get; set; }

        public string? Mobile { get; set; }

        public string? Email { get; set; }

        public string? Address { get; set; }

        public string? CompanyName { get; set; }
    }
}