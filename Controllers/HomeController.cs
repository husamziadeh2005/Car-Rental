using CarRentalPH.Data;
using CarRentalPH.Models;
using CarRentalPH.Models.ViewFind;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CarRentalPH.Controllers
{
    public class HomeController : Controller
    {
        private readonly ContextData _context;
        public HomeController(ContextData context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var vehicles = await _context.Vehicle.ToListAsync();
            var customers = await _context.Customer!.Include(c => c.Rentals).ThenInclude(r => r.Vehicle).ToListAsync();
            var rentals = await _context.Rental
                .Include(r => r.Customer)
                .Include(r => r.Vehicle)
                .OrderByDescending(r => r.StartDate)
                .ToListAsync();
            var payments = await _context.Payment.ToListAsync();

            var model = new DashboardViewModel
            {
                TotalVehicles = vehicles.Count,
                AvailableVehicles = vehicles.Count(v => v.VehicleType == EnumVehicleStatus.Available),
                RentedVehicles = vehicles.Count(v => v.VehicleType == EnumVehicleStatus.Rented),
                MaintenanceVehicles = vehicles.Count(v => v.VehicleType == EnumVehicleStatus.Maintenance),

                TotalCustomers = customers.Count,

                ActiveRentals = rentals.Count(r => r.Status == EnumRentalStatus.Active),
                PendingRentals = rentals.Count(r => r.Status == EnumRentalStatus.Pending),
                CompletedRentals = rentals.Count(r => r.Status == EnumRentalStatus.Completed),

                TotalRevenue = payments.Sum(p => p.Amount),

                RecentRentals = rentals.Take(5).ToList(),
                CustomersWithActiveRentals = customers.Where(c => c.Rentals != null && c.Rentals.Any(r => r.Status == EnumRentalStatus.Active)).ToList()
            };

            return View(model);
        }


        public IActionResult Privacy()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
