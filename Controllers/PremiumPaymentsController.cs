using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InsuranceManagmentSystem.Models;
using InsuranceManagementSystem;

public class PremiumPaymentsController : Controller
{
    private readonly ApplicationDbContext _context;

    public PremiumPaymentsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: PREMIUMPAYMENTS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.PremiumPayments.ToListAsync());
    }

    // GET: PREMIUMPAYMENTS/Details/5
    public async Task<IActionResult> Details(int? paymentid)
    {
        if (paymentid == null)
        {
            return NotFound();
        }

        var premiumpayment = await _context.PremiumPayments
            .FirstOrDefaultAsync(m => m.PaymentID == paymentid);
        if (premiumpayment == null)
        {
            return NotFound();
        }

        return View(premiumpayment);
    }

    // GET: PREMIUMPAYMENTS/Create
    public IActionResult Create()
    {
        ViewBag.PolicyID = new SelectList(_context.Policies, "PolicyID", "PolicyID");
        return View();
    }

    // POST: PREMIUMPAYMENTS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("PaymentID,PolicyID,Amount,PaymentDate,PaymentStatus")] PremiumPayment premiumpayment)
    {
        if (ModelState.IsValid)
        {
            _context.Add(premiumpayment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(premiumpayment);
    }

    // GET: PREMIUMPAYMENTS/Edit/5
    public async Task<IActionResult> Edit(int? paymentid)
    {
        if (paymentid == null)
        {
            return NotFound();
        }

        var premiumpayment = await _context.PremiumPayments.FindAsync(paymentid);
        if (premiumpayment == null)
        {
            return NotFound();
        }
        return View(premiumpayment);
    }

    // POST: PREMIUMPAYMENTS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? paymentid, [Bind("PaymentID,PolicyID,Amount,PaymentDate,PaymentStatus")] PremiumPayment premiumpayment)
    {
        if (paymentid != premiumpayment.PaymentID)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(premiumpayment);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PremiumPaymentExists(premiumpayment.PaymentID))
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
        return View(premiumpayment);
    }

    // GET: PREMIUMPAYMENTS/Delete/5
    public async Task<IActionResult> Delete(int? paymentid)
    {
        if (paymentid == null)
        {
            return NotFound();
        }

        var premiumpayment = await _context.PremiumPayments
            .FirstOrDefaultAsync(m => m.PaymentID == paymentid);
        if (premiumpayment == null)
        {
            return NotFound();
        }

        return View(premiumpayment);
    }

    // POST: PREMIUMPAYMENTS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? paymentid)
    {
        var premiumpayment = await _context.PremiumPayments.FindAsync(paymentid);
        if (premiumpayment != null)
        {
            _context.PremiumPayments.Remove(premiumpayment);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool PremiumPaymentExists(int? paymentid)
    {
        return _context.PremiumPayments.Any(e => e.PaymentID == paymentid);
    }
}
