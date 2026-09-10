namespace CarRentalPH.Models.ViewFind
{
    public class DashboardViewModel
    {
        public int TotalVehicles { get; set; }
        public int AvailableVehicles { get; set; }
        public int RentedVehicles { get; set; }
        public int MaintenanceVehicles { get; set; }

        public int TotalCustomers { get; set; }

        public int ActiveRentals { get; set; }
        public int PendingRentals { get; set; }
        public int CompletedRentals { get; set; }

        public decimal TotalRevenue { get; set; }

        public List<Rental> RecentRentals { get; set; } = new();
        public List<Customer> CustomersWithActiveRentals { get; set; } = new();
    }
}
