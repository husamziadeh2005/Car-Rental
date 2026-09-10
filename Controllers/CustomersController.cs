
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarRentalPH.Models;
using CarRentalPH.Data;
using Microsoft.AspNetCore.Authorization;

[Authorize]
[Route("[Controller]")]
[Route("Clients")]
public class CustomersController : Controller
{
    private readonly ContextData _context;

    public CustomersController(ContextData context)
    {
        _context = context;
    }

    // GET: CUSTOMERS
    [Route("")]
    [Route("[Action]")]
    public async Task<IActionResult> Index()
    {
        ViewData["Title"] = "Customers List";
        return View(await _context.Customer.ToListAsync());
    }

    // GET: CUSTOMERS/Details/5
    [Route("[Action]/{Id:int}")]
   
    public async Task<IActionResult> Details(int? Id)
    {
        ViewData["Title"] = "Details";
        if (Id == null)
        {
            return NotFound();
        }

        var customer = await _context.Customer
            .FirstOrDefaultAsync(m => m.CustomerId == Id);
        if (customer == null)
        {
            return NotFound();
        }

        return View(customer);
    }

    // GET: CUSTOMERS/Create
    [Route("[Action]")]
  
    public IActionResult Create()
    {
        ViewData["Title"] = "Create";
        return View();
    }

    // POST: CUSTOMERS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
   
    [HttpPost]
    [Route("[Action]")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("CustomerId,FirstName,LastName,Email,EmailConfirm,Phone,DriverLicenseNumber,DateOfBirth,Rentals")] Customer customer)
    {

        if (ModelState.IsValid)
        {
            _context.Add(customer);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Customer created successfully.";
            return RedirectToAction(nameof(Index));
        }
        return View(customer);
    }

    // GET: CUSTOMERS/Edit/5
    [Route("[Action]/{Id:int}")]
    [Authorize]
    public async Task<IActionResult> Edit(int? Id)
    {
        ViewData["Title"] = "Edit";

        if (Id == null)
        {
            return NotFound();
        }

        var customer = await _context.Customer.FindAsync(Id);
        if (customer == null)
        {
            return NotFound();
        }
        return View(customer);
    }

    // POST: CUSTOMERS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    
    [HttpPost]
    [Route("[Action]/{Id:int}")]
    [Authorize]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? Id, [Bind("CustomerId,FirstName,LastName,Email,EmailConfirm,Phone,DriverLicenseNumber,DateOfBirth,Rentals")] Customer customer)
    {
        if (Id != customer.CustomerId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(customer);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(customer.CustomerId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            TempData["SuccessMessage"] = "Customer updated successfully.";
            return RedirectToAction(nameof(Index));
        }
        return View(customer);
    }

    // GET: CUSTOMERS/Delete/5
    [Route("[Action]/{Id:int}")]
    public async Task<IActionResult> Delete(int? Id)
    {
        ViewData["Title"] = "Delete";

        if (Id == null)
        {
            return NotFound();
        }

        var customer = await _context.Customer
            .FirstOrDefaultAsync(m => m.CustomerId == Id);
        if (customer == null)
        {
            return NotFound();
        }

        return View(customer);
    }

    // POST: CUSTOMERS/Delete/5
    [Route("[Action]/{Id:int}")]
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? customerid)
    {
        var customer = await _context.Customer.FindAsync(customerid);
        if (customer != null)
        {
            _context.Customer.Remove(customer);
        }

        await _context.SaveChangesAsync();
        TempData["SuccessMessage"] = "Customer deleted.";
        return RedirectToAction(nameof(Index));
    }

    private bool CustomerExists(int? Id)
    {
        return _context.Customer.Any(e => e.CustomerId == Id);
    }
}