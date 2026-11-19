using InsuranceManagementSystem;
using InsuranceManagmentSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InsuranceManagementSystem.Controllers
{
    public class CompanyPremiumController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CompanyPremiumController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var companyPremium = await (
                from payment in _context.PremiumPayments
                join policy in _context.Policies
                    on payment.PolicyID equals policy.PolicyID
                join agent in _context.Agents
                    on policy.AgentID equals agent.AgentID
                where agent.CompanyName != null && agent.CompanyName != ""
                select new CompanyPremiumViewModel
                {
                    CompanyName = agent.CompanyName,
                    AgentName = agent.AgentName,
                    PolicyID = policy.PolicyID,
                    PaymentID = payment.PaymentID,
                    Amount = payment.Amount,
                    PaymentDate = payment.PaymentDate,
                    PaymentStatus = payment.PaymentStatus
                }
            ).ToListAsync();

            return View(companyPremium);
        }
    }
}