
using CarRentalPH.Data;
using CarRentalPH.Models;
using CarRentalPH.Models.ViewFind;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
[Authorize]

public class VehiclesController : Controller
{
    private readonly ContextData _context;

    public VehiclesController(ContextData context)
    {
        _context = context;
    }

   
    [HttpGet]
    public async Task<IActionResult> Find()
    {
        ViewData["Title"] = "Find Vehicle";
        return View(new FindVehicle());
    }

    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Find([Bind("VehicleId,Brand,Vehicles")] FindVehicle model)
    {
        ViewData["Title"] = "Find Vehicle";
        if (model.VehicleId != null)
        {
            model.Vehicles = await _context.Vehicle.Include(v => v.Category).Where(c => c.VehicleId == model.VehicleId).ToListAsync();
        }
        if (model.Brand != null && model.Vehicles.Count == 0)
        {
            model.Vehicles = await _context.Vehicle.Include(v => v.Category).Where(c => c.Brand.ToLower().Contains(model.Brand.ToLower())).ToListAsync();
        }
        else if (model.Brand != null && model.Vehicles.Count != 0)
        {
            model.Vehicles = model.Vehicles.Where(c => c.Brand.ToLower().Contains(model.Brand.ToLower())).ToList();

        }


        return View(model);

    }

    // GET: VEHICLES
    
    public async Task<IActionResult> Index(String sortBy = "none")
    {
        ViewData["Title"] = "Vehicle List";
        List<Vehicle> vehicles = await _context.Vehicle.Include(v => v.Category).ToListAsync();
        switch (sortBy.ToLower())
        {
            case "vehicleid_asec":
                vehicles = vehicles.OrderBy(c => c.VehicleId).ToList();
                break;
            case "vehicleid_desc":
                vehicles = vehicles.OrderByDescending(c => c.VehicleId).ToList();
                break;
            case "year_asec":
                vehicles = vehicles.OrderBy(c => c.Year).ToList();
                break;
            case "year_desc":
                vehicles = vehicles.OrderByDescending(c => c.Year).ToList();
                break;
            default:
                vehicles = vehicles.OrderBy(c => c.VehicleCategoryId).ThenBy(c => c.VehicleId).ToList();
                break;

        }
        ViewData["SortBy"] = sortBy;
        return View(vehicles);

    }

    // GET: VEHICLES/Details/5
    [Authorize] 
    public async Task<IActionResult> Details(int? Id)
    {
        ViewData["Title"] = "Details";

        if (Id == null)
        {
            return NotFound();
        }

        var vehicle = await _context.Vehicle
            .Include(v => v.Category)
            .FirstOrDefaultAsync(m => m.VehicleId == Id);
        if (vehicle == null)
        {
            return NotFound();
        }

        return View(vehicle);
    }

    // GET: VEHICLES/Create

    public async Task<IActionResult> Create()
    {
        ViewData["Title"] = "Create";
        ViewBag.Categories = new SelectList(await _context.VehicleCategory.ToListAsync(), "VehicleCategoryId", "Name");
        return View();
    }

    // POST: VEHICLES/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
 
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("VehicleId,VehicleCategoryId,Brand,Model,Year,LicensePlate,DailyRate,VehicleType")] Vehicle vehicle)
    {
        if (ModelState.IsValid)
        {
            _context.Add(vehicle);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Vehicle created successfully.";
            return RedirectToAction(nameof(Index));
        }
        ViewBag.Categories = new SelectList(await _context.VehicleCategory.ToListAsync(), "VehicleCategoryId", "Name", vehicle.VehicleCategoryId);
        return View(vehicle);
    }

    // GET: VEHICLES/Edit/5
    [Authorize]

    public async Task<IActionResult> Edit(int? Id)
    {
        ViewData["Title"] = "Edit";

        if (Id == null)
        {
            return NotFound();
        }

        var vehicle = await _context.Vehicle.FindAsync(Id);
        if (vehicle == null)
        {
            return NotFound();
        }
        ViewBag.Categories = new SelectList(await _context.VehicleCategory.ToListAsync(), "VehicleCategoryId", "Name", vehicle.VehicleCategoryId);
        return View(vehicle);
    }

    // POST: VEHICLES/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
 
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize]

    public async Task<IActionResult> Edit(int? Id, [Bind("VehicleId,VehicleCategoryId,Brand,Model,Year,LicensePlate,DailyRate,VehicleType")] Vehicle vehicle)
    {
        if (Id != vehicle.VehicleId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(vehicle);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehicleExists(vehicle.VehicleId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            TempData["SuccessMessage"] = "Vehicle updated successfully.";
            return RedirectToAction(nameof(Index));
        }
        ViewBag.Categories = new SelectList(await _context.VehicleCategory.ToListAsync(), "VehicleCategoryId", "Name", vehicle.VehicleCategoryId);
        return View(vehicle);
    }

    // GET: VEHICLES/Delete/5
    [Authorize]

    public async Task<IActionResult> Delete(int? Id)
    {
        ViewData["Title"] = "Delete";

        if (Id == null)
        {
            return NotFound();
        }

        var vehicle = await _context.Vehicle
            .Include(v => v.Category)
            .FirstOrDefaultAsync(m => m.VehicleId == Id);
        if (vehicle == null)
        {
            return NotFound();
        }

        return View(vehicle);
    }

    // POST: VEHICLES/Delete/5
  
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? Id)
    {
        var vehicle = await _context.Vehicle.FindAsync(Id);
        if (vehicle != null)
        {
            _context.Vehicle.Remove(vehicle);
        }

        await _context.SaveChangesAsync();
        TempData["SuccessMessage"] = "Vehicle deleted.";
        return RedirectToAction(nameof(Index));
    }

    private bool VehicleExists(int? Id)
    {
        return _context.Vehicle.Any(e => e.VehicleId == Id);
    }

    public async Task<IActionResult> OurCars()
    {
        ViewData["Title"] = "Our Cars";

        IEnumerable<Vehicle>? vehicles = await _context.Vehicle
                                                    .Include(v => v.Category)
                                                    .OrderBy(v => v.VehicleId)
                                                    .ToListAsync();

        return View(vehicles);
    }

}


