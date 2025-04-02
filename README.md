# 💰 Personal Finance Control API

This project is a **Personal Financial Management API** built with modern .NET technologies, following clean architecture principles, CQRS, and full test coverage.

---

## ✅ Features

- Register income and expense transactions
- List all transactions
- Get a transaction by ID
- List days where expenses exceeded income (negative balance)
- Background service that checks balance and sends email notifications
- Full test coverage with xUnit
- Logging with ILogger
- Global error handling with custom middleware

---

## 🧱 Architecture

The project follows **Clean Architecture** and **CQRS** principles:

```
PD_FOOD
│
├── PD_FOOD.Application        → Use cases, Commands, Queries, Interfaces
├── PD_FOOD.Domain             → Entities and Enums
├── PD_FOOD.Infrastructure     → EF Core, Mappings, DbContext
├── PD_FOOD.Worker             → Background email sender service
├── PD_FOOD.API                → WebAPI Controllers, Middlewares, and Configuration
└── PD_FOOD.Tests              → xUnit test project (API + Worker + Middleware)
```

---

## ⚙️ Technologies Used

| Layer              | Technologies                                           |
|--------------------|--------------------------------------------------------|
| API                | ASP.NET Core (.NET 9), Swagger                         |
| Application Layer  | CQRS with MediatR, FluentValidation                    |
| Domain             | Entities, Enums                                        |
| Infrastructure     | Entity Framework Core, PostgreSQL                      |
| Worker             | BackgroundService, SmtpClient                          |
| Logging            | ILogger<T>                                             |
| Error Handling     | Custom Exception Middleware                            |
| Testing            | xUnit, Moq, InMemoryDatabase                           |

---

## 📦 Project Setup

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
- PostgreSQL running locally (default port 5432)
- SMTP server (can be local for dev)

---

### 1. Configure `appsettings.Development.json`

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=PersonalFinanceDb;Username=your_user;Password=your_pass"
  }
}
```

---

### 2. Run the application

```bash
dotnet ef database update --project PD_FOOD.Infrastructure --startup-project PD_FOOD
dotnet run --project PD_FOOD
```

---

### 3. Swagger UI

Once running, open:

```
https://localhost:5001/swagger
```

---

## 🧪 Running Tests

```bash
dotnet test
```

- Includes tests for:
  - Controller endpoints
  - CQRS handlers
  - Background worker (`SmtpWorker`)
  - Middleware for exception handling
  - Validations (FluentValidation)

---

## 📬 Worker - Daily Balance Notification

- Background service (`SmtpWorker`) runs daily
- Sends email if expenses > income
- Email logic is abstracted via `ISmtpSender` for testability

---

## 🧾 Logging

- All actions in controllers and workers use `ILogger<T>`
- Logs include input, result count, warnings, and errors

---

## 🛑 Global Error Handling

- Middleware `ExceptionMiddleware` catches unhandled exceptions
- Returns standardized JSON errors with status code `500`

---

## 📄 License

MIT - Feel free to use and adapt this project for your own needs.
