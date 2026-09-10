
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarRentalPH.Models;
using CarRentalPH.Data;
using Microsoft.AspNetCore.Authorization;
[Authorize]

public class PaymentsController : Controller
{
    private readonly ContextData _context;

    public PaymentsController(ContextData context)
    {
        _context = context;
    }

    private async Task PopulateRentalsAsync(int? rentalId = null)
    {
        var rentals = await _context.Rental.Include(r => r.Customer).Include(r => r.Vehicle).ToListAsync();
        ViewBag.Rentals = new SelectList(
            rentals.Select(r => new
            {
                r.Id,
                Display = "#" + r.Id + " — " + (r.Customer != null ? r.Customer.FirstName + " " + r.Customer.LastName : "Customer") + " / " + (r.Vehicle != null ? r.Vehicle.Brand + " " + r.Vehicle.Model : "Vehicle")
            }), "Id", "Display", rentalId);
    }

    // GET: PAYMENTS
    public async Task<IActionResult> Index()
    {
        ViewData["Title"] = "Payment List";
        var payments = await _context.Payment.Include(p => p.Rental).ThenInclude(r => r!.Customer).ToListAsync();


        decimal averagePayment = payments.Count > 0
            ? payments.Average(p => p.Amount + (p.Amount * 0.3m))
            : 0;

        decimal maximumPayment = payments.Count > 0
           ? payments.Max(p => p.Amount + (p.Amount * 0.3m))
           : 0;

        ViewBag.AveragePayment = averagePayment;
        ViewBag.MaximumPayment = maximumPayment;

        return View(payments);
    }

    // GET: PAYMENTS/Details/5
    public async Task<IActionResult> Details(int? Id)
    {
        ViewData["Title"] = "Details";

        if (Id == null)
        {
            return NotFound();
        }

        var payment = await _context.Payment
            .Include(p => p.Rental)
            .ThenInclude(r => r!.Customer)
            .Include(p => p.Rental)
            .ThenInclude(r => r!.Vehicle)
            .FirstOrDefaultAsync(m => m.PaymentId == Id);

        if (payment == null)
        {
            return NotFound();
        }

        return View(payment);
    }

    // GET: PAYMENTS/Create

    public async Task<IActionResult> Create()
    {
        ViewData["Title"] = "Create";

        var lastRental = await _context.Rental
            .OrderByDescending(r => r.Id)
            .FirstOrDefaultAsync();

        Payment payment = new Payment();

        if (lastRental != null)
        {
            payment.RentalId = lastRental.Id;
        }

        await PopulateRentalsAsync(payment.RentalId);
        return View(payment);
    }

    // POST: PAYMENTS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]

    public async Task<IActionResult> Create([Bind("PaymentId,RentalId,Amount,PaymentDate,PaymentMethod,PaymentStatus")] Payment payment)
    {
        if (ModelState.IsValid)
        {
            _context.Add(payment);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Payment recorded successfully.";
            return RedirectToAction(nameof(Index));
        }
        await PopulateRentalsAsync(payment.RentalId);
        return View(payment);
    }

    // GET: PAYMENTS/Edit/5
    public async Task<IActionResult> Edit(int? Id)
    {
        ViewData["Title"] = "Edit";
        if (Id == null)
        {
            return NotFound();
        }

        var payment = await _context.Payment.FindAsync(Id);
        if (payment == null)
        {
            return NotFound();
        }
        await PopulateRentalsAsync(payment.RentalId);
        return View(payment);
    }

    // POST: PAYMENTS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? Id, [Bind("PaymentId,RentalId,Amount,PaymentDate,PaymentMethod,PaymentStatus")] Payment payment)
    {
        if (Id != payment.PaymentId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(payment);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentExists(payment.PaymentId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            TempData["SuccessMessage"] = "Payment updated successfully.";
            return RedirectToAction(nameof(Index));
        }
        await PopulateRentalsAsync(payment.RentalId);
        return View(payment);
    }

    // GET: PAYMENTS/Delete/5
    public async Task<IActionResult> Delete(int? Id)
    {
        ViewData["Title"] = "Delete";
        if (Id == null)
        {
            return NotFound();
        }

        var payment = await _context.Payment
            .Include(p => p.Rental)
            .FirstOrDefaultAsync(m => m.PaymentId == Id);
        if (payment == null)
        {
            return NotFound();
        }

        return View(payment);
    }

    // POST: PAYMENTS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var payment = await _context.Payment.FindAsync(id);
        if (payment != null)
        {
            _context.Payment.Remove(payment);
        }

        await _context.SaveChangesAsync();
        TempData["SuccessMessage"] = "Payment deleted.";
        return RedirectToAction(nameof(Index));
    }

    private bool PaymentExists(int? id)
    {
        return _context.Payment.Any(e => e.PaymentId == id);
    }

    public async Task<IActionResult> calculations()
    {
        ViewData["Title"] = " payment calculations";

        var payments = await _context.Payment.ToListAsync();

        decimal totalRevenue = payments.Count > 0
            ? payments.Sum(p => p.Amount + (p.Amount * 0.3m))
            : 0;

        decimal averagePayment = payments.Count > 0
            ? payments.Average(p => p.Amount + (p.Amount * 0.3m))
            : 0;

        decimal maximumPayment = payments.Count > 0
            ? payments.Max(p => p.Amount + (p.Amount * 0.3m))
            : 0;

        ViewBag.TotalPayments = payments.Count;
        ViewBag.TotalRevenue = totalRevenue;
        ViewBag.AveragePayment = averagePayment;
        ViewBag.MaximumPayment = maximumPayment;

        return View();
    }
}
