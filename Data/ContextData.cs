using CarRentalPH.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarRentalPH.Data
{
    public class ContextData(DbContextOptions<ContextData> options) : IdentityDbContext(options)
    {
        public DbSet<VehicleCategory> VehicleCategory { get; set; } = default!;
        public DbSet<Vehicle> Vehicle { get; set; } = default!;
        public DbSet<Rental> Rental { get; set; } = default!;
        public DbSet<Payment> Payment { get; set; } = default!;
        public DbSet<Customer>? Customer { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //1) VehicleCategory
            builder.Entity<VehicleCategory>().HasData(
                new VehicleCategory { VehicleCategoryId = 1, Name = "Economy", VehicleType = EnumVehicleType.Economy, MaxPassengers = 5 },
                new VehicleCategory { VehicleCategoryId = 2, Name = "SUV", VehicleType = EnumVehicleType.Suv, MaxPassengers = 7 },
                new VehicleCategory { VehicleCategoryId = 3, Name = "Luxury", VehicleType = EnumVehicleType.laxury, MaxPassengers = 5 },
                new VehicleCategory { VehicleCategoryId = 4, Name = "Van", VehicleType = EnumVehicleType.Van, MaxPassengers = 8 },
                new VehicleCategory { VehicleCategoryId = 5, Name = "Sport", VehicleType = EnumVehicleType.Sport, MaxPassengers = 2 }

                );
            //2) Customer 
            builder.Entity<Customer>().HasData(
                new Customer { CustomerId = 1, FirstName = "Ahmad", LastName = "Hasan", Email = "ahmadhasan@gmail.com", EmailConfirm = "ahmadhasan@gmail.com", Phone = "0793759834", DriverLicenseNumber = "8764587", DateOfBirth = new DateTime(1992, 5, 10) },
                new Customer { CustomerId = 2, FirstName = "Ali", LastName = "Alhamed", Email = "alialhamed@gmail.com", EmailConfirm = "alialhamed@gmail.com", Phone = "0782056732", DriverLicenseNumber = "8532971", DateOfBirth = new DateTime(2001, 3, 7) },
                new Customer { CustomerId = 3, FirstName = "Omar", LastName = "Naser", Email = "omarnaser@gmail.com", EmailConfirm = "omarnaser@gmail.com", Phone = "0787493378", DriverLicenseNumber = "2345693", DateOfBirth = new DateTime(2001, 6, 10) },
                new Customer { CustomerId = 4, FirstName = "Majed", LastName = "Hashem", Email = "majedhashem@gmail.com", EmailConfirm = "majedhashem@gmail.com", Phone = "0795831274", DriverLicenseNumber = "4287450", DateOfBirth = new DateTime(2003, 4, 9) },
                new Customer { CustomerId = 5, FirstName = "Mohammad", LastName = "Osama", Email = "mohammadosama@gmail.com", EmailConfirm = "mohammadosama@gmail.com", Phone = "0771048603", DriverLicenseNumber = "8532971", DateOfBirth = new DateTime(2000, 11, 7) }

                );
            //3) Vehicles
            builder.Entity<Vehicle>().HasData(
                new Vehicle { VehicleId = 1, VehicleCategoryId = 1, Brand = "Toyota", Model = "Corolla", Year = 2023, LicensePlate = "10-12345", DailyRate = 30, VehicleType = EnumVehicleStatus.Available },
                new Vehicle { VehicleId = 2, VehicleCategoryId = 2, Brand = "Hyundai", Model = "Santa Fe", Year = 2024, LicensePlate = "20-46960", DailyRate = 55, VehicleType = EnumVehicleStatus.Available },
                new Vehicle { VehicleId = 3, VehicleCategoryId = 3, Brand = "BMW", Model = "520i", Year = 2023, LicensePlate = "10-29004", DailyRate = 100, VehicleType = EnumVehicleStatus.Available },
                new Vehicle { VehicleId = 4, VehicleCategoryId = 4, Brand = "Toyota", Model = "Hiace", Year = 2022, LicensePlate = "64-60286", DailyRate = 70, VehicleType = EnumVehicleStatus.Available },
                new Vehicle { VehicleId = 5, VehicleCategoryId = 5, Brand = "Porsche", Model = "911", Year = 2024, LicensePlate = "39-94645", DailyRate = 200, VehicleType = EnumVehicleStatus.Available },
                new Vehicle { VehicleId = 6, VehicleCategoryId = 1, Brand = "Honda", Model = "Civic", Year = 2023, LicensePlate = "11-58213", DailyRate = 32, VehicleType = EnumVehicleStatus.Available },
                new Vehicle { VehicleId = 7, VehicleCategoryId = 1, Brand = "Kia", Model = "Rio", Year = 2022, LicensePlate = "12-77410", DailyRate = 28, VehicleType = EnumVehicleStatus.Rented },
                new Vehicle { VehicleId = 8, VehicleCategoryId = 2, Brand = "Ford", Model = "Everest", Year = 2023, LicensePlate = "21-38820", DailyRate = 60, VehicleType = EnumVehicleStatus.Available },
                new Vehicle { VehicleId = 9, VehicleCategoryId = 2, Brand = "Mitsubishi", Model = "Montero Sport", Year = 2024, LicensePlate = "22-90154", DailyRate = 58, VehicleType = EnumVehicleStatus.Maintenance },
                new Vehicle { VehicleId = 10, VehicleCategoryId = 3, Brand = "Mercedes-Benz", Model = "C200", Year = 2024, LicensePlate = "13-40772", DailyRate = 110, VehicleType = EnumVehicleStatus.Available },
                new Vehicle { VehicleId = 11, VehicleCategoryId = 4, Brand = "Nissan", Model = "Urvan", Year = 2021, LicensePlate = "64-72910", DailyRate = 68, VehicleType = EnumVehicleStatus.Available },
                new Vehicle { VehicleId = 12, VehicleCategoryId = 5, Brand = "Chevrolet", Model = "Camaro", Year = 2023, LicensePlate = "39-11287", DailyRate = 180, VehicleType = EnumVehicleStatus.Rented }
                           );

            //4) Rental
            builder.Entity<Rental>().HasData(
                new Rental { Id = 1, CustomerId = 1, VehicleId = 1, StartDate = new DateTime(2026, 8, 10), EndDate = new DateTime(2026, 8, 13), TotalPrice = 90, Status = EnumRentalStatus.Completed },
                new Rental { Id = 2, CustomerId = 2, VehicleId = 2, StartDate = new DateTime(2026, 7, 3), EndDate = new DateTime(2026, 7, 11), TotalPrice = 440, Status = EnumRentalStatus.Completed },
                new Rental { Id = 3, CustomerId = 3, VehicleId = 3, StartDate = new DateTime(2026, 8, 20), EndDate = new DateTime(2026, 10, 13), TotalPrice = 5300, Status = EnumRentalStatus.Active },
                new Rental { Id = 4, CustomerId = 4, VehicleId = 4, StartDate = new DateTime(2026, 9, 20), EndDate = new DateTime(2026, 9, 25), TotalPrice = 1000, Status = EnumRentalStatus.Pending }
                );
            //5) payment
            builder.Entity<Payment>().HasData(
                new Payment { PaymentId = 1, RentalId = 1, Amount = 165, PaymentDate = new DateTime(2026, 8, 10), PaymentMethod = "credit card", PaymentStatus = "paid" },
                new Payment { PaymentId = 2, RentalId = 2, Amount = 300, PaymentDate = new DateTime(2026, 7, 3), PaymentMethod = "cash", PaymentStatus = "paid" },
                new Payment { PaymentId = 3, RentalId = 3, Amount = 700, PaymentDate = new DateTime(2026, 8, 20), PaymentMethod = "credit card", PaymentStatus = "paid" },
                new Payment { PaymentId = 4, RentalId = 4, Amount = 220, PaymentDate = new DateTime(2026, 9, 20), PaymentMethod = "cash", PaymentStatus = "pending" }
            );

        }



    }


}
