namespace InsuranceManagmentSystem.Models
{
    public class Customer
    {
        public int CustomerID { get; set; }
        public string Name { get; set; } = " ";
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; } = " ";
        public string Mobile { get; set; } = " ";
        public string Email { get; set; } = " ";
        public string Address { get; set; } = " ";
    }
}
