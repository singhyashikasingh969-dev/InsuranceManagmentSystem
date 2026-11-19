using InsuranceManagementSystem;
using InsuranceManagmentSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

public class PoliciesController : Controller
{
    private readonly ApplicationDbContext _context;

    public PoliciesController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Policies
    public async Task<IActionResult> Index()
    {
        var policies = await _context.Policies
            .ToListAsync();

        ViewBag.Agents = await _context.Agents
            .ToDictionaryAsync(a => a.AgentID, a => a.AgentName);

        return View(policies);
    }

    // GET: Policies/Details/5
    public async Task<IActionResult> Details(int? policyid)
    {
        if (policyid == null)
        {
            return NotFound();
        }

        var policy = await _context.Policies
            .FirstOrDefaultAsync(m => m.PolicyID == policyid);

        if (policy == null)
        {
            return NotFound();
        }

        return View(policy);
    }

    // GET: Policies/Create
    public IActionResult Create()
    {
        ViewBag.CustomerID = new SelectList(
            _context.Customers,
            "CustomerID",
            "Name"
        );

        ViewBag.AgentID = new SelectList(
            _context.Agents,
            "AgentID",
            "AgentName"
        );

        return View();
    }

    // POST: Policies/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("PolicyID,CustomerID,AgentID,PolicyType,PremiumAmount,CoverageAmount,StartDate,EndDate")]
        Policy policy)
    {
        if (ModelState.IsValid)
        {
            _context.Add(policy);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        ViewBag.CustomerID = new SelectList(
            _context.Customers,
            "CustomerID",
            "Name",
            policy.CustomerID
        );

        ViewBag.AgentID = new SelectList(
            _context.Agents,
            "AgentID",
            "AgentName",
            policy.AgentID
        );

        return View(policy);
    }

    // GET: Policies/Edit/5
    public async Task<IActionResult> Edit(int? policyid)
    {
        if (policyid == null)
        {
            return NotFound();
        }

        var policy = await _context.Policies
            .FindAsync(policyid);

        if (policy == null)
        {
            return NotFound();
        }

        ViewBag.CustomerID = new SelectList(
            _context.Customers,
            "CustomerID",
            "Name",
            policy.CustomerID
        );

        ViewBag.AgentID = new SelectList(
            _context.Agents,
            "AgentID",
            "AgentName",
            policy.AgentID
        );

        return View(policy);
    }

    // POST: Policies/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        int? policyid,
        [Bind("PolicyID,CustomerID,AgentID,PolicyType,PremiumAmount,CoverageAmount,StartDate,EndDate")]
        Policy policy)
    {
        if (policyid != policy.PolicyID)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(policy);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PolicyExists(policy.PolicyID))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        ViewBag.CustomerID = new SelectList(
            _context.Customers,
            "CustomerID",
            "Name",
            policy.CustomerID
        );

        ViewBag.AgentID = new SelectList(
            _context.Agents,
            "AgentID",
            "AgentName",
            policy.AgentID
        );

        return View(policy);
    }

    // GET: Policies/Delete/5
    public async Task<IActionResult> Delete(int? policyid)
    {
        if (policyid == null)
        {
            return NotFound();
        }

        var policy = await _context.Policies
            .FirstOrDefaultAsync(m => m.PolicyID == policyid);

        if (policy == null)
        {
            return NotFound();
        }

        return View(policy);
    }

    // POST: Policies/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? policyid)
    {
        if (policyid == null)
        {
            return NotFound();
        }

        // Check whether premium payment exists for this policy
        var paymentExists = await _context.PremiumPayments
            .AnyAsync(p => p.PolicyID == policyid);

        if (paymentExists)
        {
            TempData["DeleteError"] =
                "This policy cannot be deleted because premium payment records are associated with it.";

            return RedirectToAction(nameof(Index));
        }

        // Check whether claim exists for this policy
        var claimExists = await _context.Claims
            .AnyAsync(c => c.PolicyID == policyid);

        if (claimExists)
        {
            TempData["DeleteError"] =
                "This policy cannot be deleted because claim records are associated with it.";

            return RedirectToAction(nameof(Index));
        }

        var policy = await _context.Policies
            .FindAsync(policyid);

        if (policy == null)
        {
            return NotFound();
        }

        _context.Policies.Remove(policy);
        await _context.SaveChangesAsync();

        TempData["SuccessMessage"] =
            "Policy deleted successfully.";

        return RedirectToAction(nameof(Index));
    }

    private bool PolicyExists(int policyid)
    {
        return _context.Policies
            .Any(e => e.PolicyID == policyid);
    }
}