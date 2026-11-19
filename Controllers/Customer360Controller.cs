using InsuranceManagementSystem;
using InsuranceManagmentSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InsuranceManagmentSystem.Controllers
{
    public class Customer360Controller : Controller
    {
        private readonly ApplicationDbContext _context;

        public Customer360Controller(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? customerId)
        {
            var customers = await _context.Customers.ToListAsync();

            ViewBag.Customers = customers;

            if (customerId == null)
            {
                return View();
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.CustomerID == customerId);

            if (customer == null)
            {
                return NotFound();
            }

            // Customer ki policies
            var policies = await _context.Policies
                .Where(p => p.CustomerID == customerId)
                .ToListAsync();

            var policyIds = policies
                .Select(p => p.PolicyID)
                .ToList();

            // Premium payments
            var payments = await _context.PremiumPayments
                .Where(p => policyIds.Contains(p.PolicyID))
                .ToListAsync();

            // Claims
            var claims = await _context.Claims
                .Where(c => policyIds.Contains(c.PolicyID))
                .ToListAsync();

            // Total Coverage Amount
            var totalCoverage = policies
                .Sum(p => p.CoverageAmount);

            // Sirf Approved claims ko coverage se subtract karna hai
            var totalApprovedClaims = claims
                .Where(c => c.ClaimStatus.Trim().Equals("Approved",
                    StringComparison.OrdinalIgnoreCase))
                .Sum(c => c.ClaimAmount);

            // Remaining Coverage
            var remainingCoverage = totalCoverage - totalApprovedClaims;

           
            if (remainingCoverage < 0)
            {
                remainingCoverage = 0;
            }

            ViewBag.Customer = customer;
            ViewBag.Policies = policies;
            ViewBag.Payments = payments;
            ViewBag.Claims = claims;

            ViewBag.SelectedCustomerId = customerId;

            // Customer 360 calculations
            ViewBag.TotalCoverage = totalCoverage;
            ViewBag.TotalApprovedClaims = totalApprovedClaims;
            ViewBag.RemainingCoverage = remainingCoverage;

            return View();
        }
    }
}