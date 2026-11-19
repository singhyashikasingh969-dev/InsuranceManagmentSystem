using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InsuranceManagmentSystem.Models;
using InsuranceManagementSystem;

public class AgentsController : Controller
{
    private readonly ApplicationDbContext _context;

    public AgentsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Agents
    public async Task<IActionResult> Index()
    {
        return View(await _context.Agents.ToListAsync());
    }

    // GET: Agents/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var agent = await _context.Agents
            .FirstOrDefaultAsync(a => a.AgentID == id);

        if (agent == null)
        {
            return NotFound();
        }

        return View(agent);
    }

    // GET: Agents/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Agents/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("AgentID,AgentName,Mobile,Email,Address,CompanyName")] Agent agent)
    {
        if (ModelState.IsValid)
        {
            _context.Agents.Add(agent);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        return View(agent);
    }

    // GET: Agents/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var agent = await _context.Agents.FindAsync(id);

        if (agent == null)
        {
            return NotFound();
        }

        return View(agent);
    }

    // POST: Agents/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        int id,
        [Bind("AgentID,AgentName,Mobile,Email,Address,CompanyName")] Agent agent)
    {
        if (id != agent.AgentID)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Agents.Update(agent);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AgentExists(agent.AgentID))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        return View(agent);
    }

    // GET: Agents/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var agent = await _context.Agents
            .FirstOrDefaultAsync(a => a.AgentID == id);

        if (agent == null)
        {
            return NotFound();
        }

        return View(agent);
    }

    // POST: Agents/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var agent = await _context.Agents.FindAsync(id);

        if (agent != null)
        {
            _context.Agents.Remove(agent);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    private bool AgentExists(int id)
    {
        return _context.Agents.Any(e => e.AgentID == id);
    }
}