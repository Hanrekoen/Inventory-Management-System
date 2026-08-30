# Inventory Management System

A desktop inventory application built with **C# and WPF**, backed by **SQL Server**. It tracks products, suppliers and sales, and reports on them.

Built while working through a C# masterclass, then extended into a full multi-page application with an MVVM architecture.

---

## Features

- **Products** — add, edit and delete stock items, each linked to a supplier, with category, quantity and price.
- **Suppliers** — maintain supplier records with contact name, phone and email.
- **Sales** — record sales against a product, with quantity, date and total.
- **Dashboard** — an at-a-glance view of current inventory state.
- **Reports** — export data for use outside the application.

---

## Architecture

The project follows the **MVVM** pattern, so the UI and the logic behind it stay separate:

```
Models/         Product, Sale, Supplier  - plain data classes
ViewModels/     one per screen           - state and commands
Pages/          XAML views                - Dashboard, Products, Suppliers, Sales, Reports
Data/           InventoryDbContext        - all database access
Helpers/        RelayCommand              - ICommand implementation for binding
```

Database access is **ADO.NET** (`SqlConnection`, `SqlCommand`, `SqlDataAdapter`) rather than an ORM, so every query is written by hand. All queries are parameterised.

---

## Database

Three tables, related by foreign key:

| Table | Key | Links to |
| --- | --- | --- |
| `Suppliers` | `SupplierID` | — |
| `Products` | `ProductID` | `SupplierID` → Suppliers |
| `Sales` | `SaleID` | `ProductID` → Products |

---

## Setup

**Requirements:** Visual Studio, .NET Framework 4.7.2, SQL Server.

1. Clone the repository and open `Inventory Management System.sln`.
2. Create a SQL Server database named `InventoryDB` with the three tables above.
3. Copy `connections.config.example` to `connections.config` and fill in your own server and credentials:

   ```xml
   <add name="InventoryDB"
        connectionString="Server=YOUR_SERVER;Database=InventoryDB;User Id=YOUR_USER;Password=YOUR_PASSWORD;"
        providerName="System.Data.SqlClient" />
   ```

   `connections.config` is gitignored, so your credentials stay off GitHub.
4. Restore NuGet packages and run.

---

## Built with

C# · WPF · XAML · .NET Framework 4.7.2 · SQL Server · ADO.NET · MVVM
ClosedXML and iTextSharp for Excel and PDF export

---

## Author

**Hanré Koen** — [@Hanrekoen](https://github.com/Hanrekoen)
