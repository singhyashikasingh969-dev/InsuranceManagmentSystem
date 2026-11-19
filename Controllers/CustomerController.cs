using InsuranceManagmentSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InsuranceManagementSystem.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Customer/Index
        public IActionResult Index()
        {
            return View();
        }

        // POST: Customer/Index
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();

                TempData["SuccesMessage"] =
                    "Welcome! Your customer form has been successfully created.";

                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Customer/Details
        public IActionResult Details()
        {
            var customers = _context.Customers.ToList();

            return View(customers);
        }

        // GET: Customer/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            [Bind("CustomerID,Name,DateOfBirth,Gender,Mobile,Email,Address")]
            Customer customer)
        {
            if (id != customer.CustomerID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Customers.Update(customer);

                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] =
                        "Customer details updated successfully.";

                    return RedirectToAction("Details");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Customers.Any(
                        c => c.CustomerID == customer.CustomerID))
                    {
                        return NotFound();
                    }

                    throw;
                }
            }

            return View(customer);
        }

        // GET: Customer/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Customer ki saari policies find karo
            var policies = await _context.Policies
                .Where(p => p.CustomerID == id)
                .ToListAsync();

            // Har policy ke payments aur claims delete karo
            foreach (var policy in policies)
            {
                var payments = await _context.PremiumPayments
                    .Where(p => p.PolicyID == policy.PolicyID)
                    .ToListAsync();

                var claims = await _context.Claims
                    .Where(c => c.PolicyID == policy.PolicyID)
                    .ToListAsync();

                // Premium Payments delete
                _context.PremiumPayments.RemoveRange(payments);

                // Claims delete
                _context.Claims.RemoveRange(claims);
            }

            // Policies delete
            _context.Policies.RemoveRange(policies);

            // Customer delete
            var customer = await _context.Customers
                .FindAsync(id);

            if (customer != null)
            {
                _context.Customers.Remove(customer);
            }

            // Database mein changes save
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] =
                "Customer and all related policy, payment and claim records deleted successfully.";

            return RedirectToAction("Details");
        }
    }
}