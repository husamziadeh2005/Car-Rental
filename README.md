# 🚗 CarRentalPH

A full-stack car rental management system built with **ASP.NET Core MVC**, **Entity Framework Core**, and **SQL Server** — featuring a modern, responsive dashboard UI for managing a vehicle fleet, customers, rentals, and payments.

![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet)
![EF Core](https://img.shields.io/badge/EF%20Core-10.0-512BD4)
![SQL Server](https://img.shields.io/badge/SQL%20Server-2022-CC2927?logo=microsoftsqlserver)
![License](https://img.shields.io/badge/license-MIT-green)

---

## ✨ Features

- **Dashboard** — live overview of fleet status, active rentals, customers, and total revenue
- **Vehicle management** — full CRUD, category-based organization, search/find by ID or brand, and a public "Our Cars" showcase page with illustrated vehicle art per category
- **Customer management** — CRUD with contact info and driver's license tracking
- **Rental management** — book vehicles to customers with date ranges, pricing, and status tracking (Pending / Active / Completed / Cancelled)
- **Payment management** — record payments per rental, with a payment insights page (totals, averages, highest payment)
- **Authentication** — ASP.NET Core Identity (login, registration, account management), with protected admin actions
- **Responsive UI** — sidebar navigation on desktop, collapsible mobile nav, consistent design system (cards, tables, badges, forms, empty/loading/error states)

---

## 🛠 Tech Stack

| Layer | Technology |
|---|---|
| Framework | ASP.NET Core MVC (.NET 10) |
| ORM | Entity Framework Core 10 |
| Database | SQL Server |
| Auth | ASP.NET Core Identity |
| Frontend | Razor Views, Bootstrap 5, custom CSS design system, vanilla JS |

---

## 📁 Project Structure

```
CarRental/
├── Controllers/          # MVC controllers (Vehicles, Rentals, Customers, Payments, VehicleCategories, Home)
├── Models/                # Entity models + enums (EnumVehicleStatus, EnumVehicleType, EnumRentalStatus)
│   └── ViewFind/          # View-specific models (search, dashboard)
├── Data/
│   ├── ContextData.cs     # EF Core DbContext + seed data
│   └── Migrations/        # EF Core migrations
├── Views/
│   ├── Home/               # Dashboard, Privacy
│   ├── Vehicles/           # List, Find, Our Cars, CRUD
│   ├── VehicleCategories/  # CRUD
│   ├── Customers/          # CRUD
│   ├── Rentals/            # CRUD
│   ├── Payments/           # CRUD + Insights
│   └── Shared/              # Layout, partials (status badges, vehicle card/image)
├── Areas/Identity/        # ASP.NET Identity pages (Login, Register, Manage)
└── wwwroot/
    ├── css/theme.css       # Design system
    └── js/site.js          # Sidebar toggle, active nav highlighting
```

---

## 🗄 Data Model

| Entity | Description |
|---|---|
| `Vehicle` | Brand, model, year, license plate, daily rate, status, category |
| `VehicleCategory` | Groups vehicles (Economy, SUV, Luxury, Van, Sport) with max passengers |
| `Customer` | Name, email, phone, driver's license, date of birth |
| `Rental` | Links a `Customer` to a `Vehicle` for a date range with total price and status |
| `Payment` | Payment recorded against a `Rental` (amount, date, method, status) |

---

## 🚀 Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- SQL Server (local instance, Docker container, or Azure SQL)
- (Optional) EF Core CLI tools: `dotnet tool install --global dotnet-ef`

### 1. Clone the repository

```bash
git clone https://github.com/<your-username>/CarRentalPH.git
cd CarRentalPH/CarRental
```

### 2. Configure the database connection

Update the connection string in `appsettings.json` (or better, set it via [user secrets](https://learn.microsoft.com/aspnet/core/security/app-secrets) for local dev):

```json
{
  "ConnectionStrings": {
    "SQLCon": "Server=localhost,1433;Database=CarRental_DB;User Id=sa;Password=YourStrongPassword123!;TrustServerCertificate=True"
  }
}
```

Using Docker for SQL Server locally:

```bash
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=YourStrongPassword123!" \
  -p 1433:1433 --name sql1 -d mcr.microsoft.com/mssql/server:2022-latest
```

### 3. Restore dependencies

```bash
dotnet restore
```

### 4. Apply migrations & seed the database

```bash
dotnet ef database update
```

### 5. Run the app

```bash
dotnet run
```

Then open the URL printed in the console (e.g. `https://localhost:5001`).

---

## 🔑 Authentication

The app uses ASP.NET Core Identity. Register a new account from the **Register** page to access protected admin actions (create/edit/delete). Browsing vehicles, categories, and the "Our Cars" page is available without logging in.

---
