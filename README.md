```markdown
# 📦 Intelligent Inventory Allocation Engine

An enterprise-grade Inventory Management System built with **.NET 8**, leveraging **Clean Architecture** and **Domain-Driven Design (DDD)** principles. This system is designed to handle complex inventory logic, real-time stock movements, and automated reorder alerts.

---

## 🚀 Key Features

- **Inventory Allocation:** Advanced logic to allocate stock across multiple warehouses with strict validation.
- **Stock Movement Tracking:** Full audit trail for every item moving in or out of the system.
- **Low-Stock Analytics:** Automated alerts when products fall below their defined reorder thresholds.
- **Health Monitoring:** Built-in health checks for API and Database connectivity.
- **Clean Architecture:** Strict separation of concerns (Domain, Application, Infrastructure, and API).
- **CQRS Pattern:** Optimized data flow using MediatR for handling Commands and Queries.

---

## 🛠️ Tech Stack

- **Framework:** .NET 8 (Web API)
- **Database:** SQL Server
- **ORM:** Entity Framework Core
- **Logging:** Serilog (with Console & File Sinks)
- **Monitoring:** ASP.NET Core Health Checks
- **Patterns:** CQRS, Repository Pattern, Unit of Work, Modular Monolith.

---

## 🏗️ Architecture Overview

The project follows the **Clean Architecture** pattern to ensure maintainability and testability:

1.  **Domain:** Contains Entities, Value Objects, and Domain Exceptions.
2.  **Application:** Contains Business Logic, DTOs, MediatR Handlers, and Interfaces.
3.  **Infrastructure:** Data access (EF Core), Migrations, and external services.
4.  **Api:** Controllers, Middlewares, and Dependency Injection setup.

---

## 🚦 Getting Started

### Prerequisites

- .NET 8 SDK
- SQL Server (LocalDB or Express)
- Visual Studio 2022

### Installation & Setup

1. **Clone the repository:**
   ```bash
   git clone [https://github.com/YourUsername/Intelligent-Inventory-Allocation-Engine.git](https://github.com/YourUsername/Intelligent-Inventory-Allocation-Engine.git)
   ```
2. **Update Connection String:**
   Navigate to `src/InventoryWarehouseSystem.Api/appsettings.json` and update your SQL Server connection.

3. **Apply Migrations:**

   ```bash
   dotnet ef database update --project ../InventoryWarehouseSystem.Infrastructure --startup-project .
   ```

4. **Run the Application:**
   ```bash
   dotnet run --project src/InventoryWarehouseSystem.Api
   ```

---

## 🧪 Testing with Postman

1. Open **Swagger** at: `http://localhost:5088/swagger`
2. Check **System Health**: `GET /api/health`
3. Try **Stock Allocation**:
   - `POST /api/v1/StockMovements/allocate`
   - Payload:
     ```json
     {
       "productId": 1,
       "warehouseId": 1,
       "quantity": 10,
       "reason": "Initial Stock"
     }
     ```

---

## 📝 License

This project is licensed under the MIT License.

---

**Developed by [Ahmed Farouk Elhadidy]** _Backend Software Engineer | .NET Specialist_
