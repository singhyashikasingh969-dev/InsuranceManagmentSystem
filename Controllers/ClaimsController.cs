using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InsuranceManagmentSystem.Models;
using InsuranceManagementSystem;

public class ClaimsController : Controller
{
    private readonly ApplicationDbContext _context;

    public ClaimsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: CLAIMS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Claims.ToListAsync());
    }

    // GET: CLAIMS/Details/5
    public async Task<IActionResult> Details(int? claimid)
    {
        if (claimid == null)
        {
            return NotFound();
        }

        var claim = await _context.Claims
            .FirstOrDefaultAsync(m => m.ClaimID == claimid);
        if (claim == null)
        {
            return NotFound();
        }

        return View(claim);
    }

    // GET: CLAIMS/Create
    public IActionResult Create()
    {
        ViewBag.PolicyID = new SelectList(_context.Policies, "PolicyID", "PolicyID");
        return View();
    }
    // POST: CLAIMS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("ClaimID,PolicyID,ClaimDate,ClaimAmount,ClaimReason,ClaimStatus")] Claim claim)
    {
        if (ModelState.IsValid)
        {
            _context.Add(claim);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(claim);
    }

    // GET: CLAIMS/Edit/5
    public async Task<IActionResult> Edit(int? claimid)
    {
        if (claimid == null)
        {
            return NotFound();
        }

        var claim = await _context.Claims.FindAsync(claimid);
        if (claim == null)
        {
            return NotFound();
        }
        return View(claim);
    }

    // POST: CLAIMS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? claimid, [Bind("ClaimID,PolicyID,ClaimDate,ClaimAmount,ClaimReason,ClaimStatus")] Claim claim)
    {
        if (claimid != claim.ClaimID)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(claim);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClaimExists(claim.ClaimID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(claim);
    }

    // GET: CLAIMS/Delete/5
    public async Task<IActionResult> Delete(int? claimid)
    {
        if (claimid == null)
        {
            return NotFound();
        }

        var claim = await _context.Claims
            .FirstOrDefaultAsync(m => m.ClaimID == claimid);
        if (claim == null)
        {
            return NotFound();
        }

        return View(claim);
    }

    // POST: CLAIMS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? claimid)
    {
        var claim = await _context.Claims.FindAsync(claimid);
        if (claim != null)
        {
            _context.Claims.Remove(claim);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ClaimExists(int? claimid)
    {
        return _context.Claims.Any(e => e.ClaimID == claimid);
    }
}
