using Microsoft.EntityFrameworkCore;
using InsuranceManagmentSystem.Models;
namespace InsuranceManagementSystem;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Policy> Policies { get; set; }
    public DbSet<PremiumPayment> PremiumPayments { get; set; }
    public DbSet<Claim> Claims { get; set; }
    public DbSet<Agent> Agents { get; set; }
}