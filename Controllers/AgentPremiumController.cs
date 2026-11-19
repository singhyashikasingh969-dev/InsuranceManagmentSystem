using InsuranceManagementSystem;
using InsuranceManagmentSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InsuranceManagmentSystem.Controllers
{
    public class AgentPremiumController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AgentPremiumController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var agentPremium = await (
                from payment in _context.PremiumPayments
                join policy in _context.Policies
                    on payment.PolicyID equals policy.PolicyID
                join agent in _context.Agents
                    on policy.AgentID equals agent.AgentID
                select new AgentPremiumViewModel
                {
                    AgentName = agent.AgentName,
                    PolicyID = policy.PolicyID,
                    PaymentID = payment.PaymentID,
                    Amount = payment.Amount,
                    PaymentDate = payment.PaymentDate,
                    PaymentStatus = payment.PaymentStatus
                }
            ).ToListAsync();

            return View(agentPremium);
        }
    }
}