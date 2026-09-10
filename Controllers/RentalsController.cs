
using CarRentalPH.Data;
using CarRentalPH.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
[Authorize]

public class RentalsController : Controller
{
    private readonly ContextData _context;

    public RentalsController(ContextData context)
    {
        _context = context;
    }

    private async Task PopulateDropdownsAsync(int? customerId = null, int? vehicleId = null)
    {
        var customers = await _context.Customer!.ToListAsync();
        ViewBag.Customers = new SelectList(customers.Select(c => new { c.CustomerId, FullName = c.FirstName + " " + c.LastName }), "CustomerId", "FullName", customerId);
        var vehicles = await _context.Vehicle.ToListAsync();
        ViewBag.Vehicles = new SelectList(vehicles.Select(v => new { v.VehicleId, Display = v.Brand + " " + v.Model + " (" + v.LicensePlate + ")" }), "VehicleId", "Display", vehicleId);
    }

    // GET: RENTALS
    
    public async Task<IActionResult> Index()
    {
        ViewData["Title"] = "Rental List";

        return View(await _context.Rental.Include(r => r.Customer).Include(r => r.Vehicle).OrderByDescending(r => r.StartDate).ToListAsync());
    }

    // GET: RENTALS/Details/5
  
    public async Task<IActionResult> Details(int? Id)
    {
        ViewData["Title"] = "Details";

        if (Id == null)
        {
            return NotFound();
        }

        var rental = await _context.Rental
            .Include(r => r.Customer)
            .Include(r => r.Vehicle)
            .Include(r => r.Payments)
            .FirstOrDefaultAsync(m => m.Id == Id);
        if (rental == null)
        {
            return NotFound();
        }

        return View(rental);
    }

    // GET: RENTALS/Create
    
    public async Task<IActionResult> Create()
    {
        ViewData["Title"] = "Create";
        await PopulateDropdownsAsync();
        return View();
    }

    // POST: RENTALS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
   
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,CustomerId,VehicleId,StartDate,EndDate,TotalPrice,Status")] Rental rental)
    {
        if (ModelState.IsValid)
        {
            _context.Add(rental);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Rental created successfully.";
            return RedirectToAction(nameof(Index));
        }
        await PopulateDropdownsAsync(rental.CustomerId, rental.VehicleId);
        return View(rental);
    }

    // GET: RENTALS/Edit/5
   
    public async Task<IActionResult> Edit(int? Id)
    {
        ViewData["Title"] = "Edit";
        if (Id == null)
        {
            return NotFound();
        }

        var rental = await _context.Rental.FindAsync(Id);
        if (rental == null)
        {
            return NotFound();
        }
        await PopulateDropdownsAsync(rental.CustomerId, rental.VehicleId);
        return View(rental);
    }

    // POST: RENTALS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? Id, [Bind("Id,CustomerId,VehicleId,StartDate,EndDate,TotalPrice,Status")] Rental rental)
    {
        if (Id != rental.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(rental);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RentalExists(rental.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            TempData["SuccessMessage"] = "Rental updated successfully.";
            return RedirectToAction(nameof(Index));
        }
        await PopulateDropdownsAsync(rental.CustomerId, rental.VehicleId);
        return View(rental);
    }

    // GET: RENTALS/Delete/5
   
    public async Task<IActionResult> Delete(int? Id)
    {
        ViewData["Title"] = "Delete";

        if (Id == null)
        {
            return NotFound();
        }

        var rental = await _context.Rental
            .Include(r => r.Customer)
            .Include(r => r.Vehicle)
            .FirstOrDefaultAsync(m => m.Id == Id);
        if (rental == null)
        {
            return NotFound();
        }

        return View(rental);
    }

    // POST: RENTALS/Delete/5
   
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? Id)
    {
        var rental = await _context.Rental.FindAsync(Id);
        if (rental != null)
        {
            _context.Rental.Remove(rental);
        }

        await _context.SaveChangesAsync();
        TempData["SuccessMessage"] = "Rental deleted.";
        return RedirectToAction(nameof(Index));
    }

    private bool RentalExists(int? Id)
    {
        return _context.Rental.Any(e => e.Id == Id);
    }
}
