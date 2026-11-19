using InsuranceManagementSystem;
using InsuranceManagmentSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace InsuranceManagmentSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Index", "Login");
        }

        public IActionResult Dashboard()
        {
            ViewBag.TotalCustomers = _context.Customers.Count();
            ViewBag.TotalPolicies = _context.Policies.Count();
            ViewBag.TotalPayments = _context.PremiumPayments.Count();
            ViewBag.TotalClaims = _context.Claims.Count();

            ViewBag.TotalPremiumAmount = _context.PremiumPayments
                .Sum(p => (decimal?)p.Amount) ?? 0;

            ViewBag.TotalClaimAmount = _context.Claims
                .Sum(c => (decimal?)c.ClaimAmount) ?? 0;

            ViewBag.PendingClaims = _context.Claims
                .Count(c => c.ClaimStatus == "Pending");

            ViewBag.ApprovedClaims = _context.Claims
               .Count(c => c.ClaimStatus == "Approved");

            ViewBag.RejectedClaims = _context.Claims
                .Count(c => c.ClaimStatus == "Rejected");

            var claimDetails = _context.Claims
    .Join(
        _context.Policies,
        claim => claim.PolicyID,
        policy => policy.PolicyID,
        (claim, policy) => new { claim, policy }
    )
    .Join(
        _context.Customers,
        x => x.policy.CustomerID,
        customer => customer.CustomerID,
        (x, customer) => new
        {
            x.claim.ClaimID,
            CustomerName = customer.Name,
            x.claim.PolicyID,
            x.claim.ClaimAmount,
            x.claim.ClaimDate,
            x.claim.ClaimReason,
            x.claim.ClaimStatus
        }
    )
    .ToList();

            ViewBag.ClaimDetails = claimDetails;

            var customerDetails = _context.Customers
    .ToList();

            ViewBag.CustomerDetails = customerDetails;

            return View("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}