
using CarRentalPH.Data;
using CarRentalPH.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
[Authorize]

public class VehicleCategoriesController : Controller
{
    private readonly ContextData _context;

    public VehicleCategoriesController(ContextData context)
    {
        _context = context;
    }

    // GET: VEHICLECATEGORYS
   
    public async Task<IActionResult> Index()
    {
        ViewData["Title"] = "Vehicle Category List";

        return View(await _context.VehicleCategory.ToListAsync());
    }

    // GET: VEHICLECATEGORYS/Details/5
   
    public async Task<IActionResult> Details(int? id)
    {
        ViewData["Title"] = "Details";

        if (id == null)
        {
            return NotFound();
        }

        var vehicleCategory = await _context.VehicleCategory
            .Include(vc => vc.Vehicles)
            .FirstOrDefaultAsync(m => m.VehicleCategoryId == id);

        if (vehicleCategory == null)
        {
            return NotFound();
        }

        return View(vehicleCategory);
    }

    // GET: VEHICLECATEGORYS/Create
   
    public IActionResult Create()
    {
        ViewData["Title"] = "Create";
        return View();
    }

    // POST: VEHICLECATEGORYS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("VehicleCategoryId,Name,VehicleType,MaxPassengers,Vehicles")] VehicleCategory vehiclecategory)
    {
        if (ModelState.IsValid)
        {
            _context.Add(vehiclecategory);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Category created successfully.";
            return RedirectToAction(nameof(Index));
        }
        return View(vehiclecategory);
    }

    // GET: VEHICLECATEGORYS/Edit/5
  
    public async Task<IActionResult> Edit(int? Id)
    {
        ViewData["Title"] = "Edit";

        if (Id == null)
        {
            return NotFound();
        }

        var vehiclecategory = await _context.VehicleCategory.FindAsync(Id);
        if (vehiclecategory == null)
        {
            return NotFound();
        }
        return View(vehiclecategory);
    }

    // POST: VEHICLECATEGORYS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? Id, [Bind("VehicleCategoryId,Name,VehicleType,MaxPassengers,Vehicles")] VehicleCategory vehiclecategory)
    {
        if (Id != vehiclecategory.VehicleCategoryId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(vehiclecategory);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehicleCategoryExists(vehiclecategory.VehicleCategoryId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            TempData["SuccessMessage"] = "Category updated successfully.";
            return RedirectToAction(nameof(Index));
        }
        return View(vehiclecategory);
    }

    // GET: VEHICLECATEGORYS/Delete/5
  
    public async Task<IActionResult> Delete(int? Id)
    {
        ViewData["Title"] = "Delete";
        if (Id == null)
        {
            return NotFound();
        }

        var vehiclecategory = await _context.VehicleCategory
            .FirstOrDefaultAsync(m => m.VehicleCategoryId == Id);
        if (vehiclecategory == null)
        {
            return NotFound();
        }

        return View(vehiclecategory);
    }

    // POST: VEHICLECATEGORYS/Delete/5
    
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? Id)
    {
        var vehiclecategory = await _context.VehicleCategory.FindAsync(Id);
        if (vehiclecategory != null)
        {
            _context.VehicleCategory.Remove(vehiclecategory);
        }

        await _context.SaveChangesAsync();
        TempData["SuccessMessage"] = "Category deleted.";
        return RedirectToAction(nameof(Index));
    }

    private bool VehicleCategoryExists(int? Id)
    {
        return _context.VehicleCategory.Any(e => e.VehicleCategoryId == Id);
    }
}