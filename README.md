<div align="center">

# Client Hub

> A client management web app, rebuilt from scratch as a deliberate C#/.NET learning project — MVC,
> EF Core and MySQL, with cookie authentication, per-user ownership and no shortcuts on security.

<p align="center">

[![License: MIT](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)
[![.NET](https://img.shields.io/badge/.NET-10-512bd4.svg)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-13-239120.svg)](https://learn.microsoft.com/en-us/dotnet/csharp/)
[![MySQL](https://img.shields.io/badge/MySQL-8-4479A1.svg)](https://www.mysql.com/)
[![Status](https://img.shields.io/badge/status-MVP-512bd4.svg)]

</p>
</div>

---

## About

This project was originally developed in **2025** as part of my Professional Course.

In **2026**, I rebuilt the application from scratch to apply what I learned since the original
version. The rebuild deliberately fixes the original project's most important flaws: custom
password hashing, overposting, and no per-user isolation.

## Features

- Cookie-based authentication (register, login, logout) with PBKDF2 password hashing
- Per-user client ownership enforced on the server
- Client CRUD (list, details, create, edit, delete)
- Search, filtering by postal code, sorting and pagination — resolved in SQL
- Minimal dashboard with real numbers
- Professional 404/500 error handling
- Validated forms (client-side and server-side) and overposting protection

## Tech Stack

- C# / .NET 10
- ASP.NET Core MVC (Controllers + Razor Views, not an API)
- Entity Framework Core + MySQL (`MySql.EntityFrameworkCore`)
- Cookie Authentication
- Bootstrap 5 (responsive)
- xUnit (service-level tests)

## Architecture

```
Browser
   │  HTML forms, cookies
   ▼
ASP.NET Core Middleware  (routing, cookie auth, error/status pages)
   │
   ▼
Controller
   │  binds request → ViewModel, checks ModelState
   ▼
Service   (IClientService, IAuthService — the only layer that talks to EF Core)
   │
   ▼
EF Core   (ApplicationDbContext)
   │
   ▼
MySQL
```

```
ClientHub/
│
├── Controllers/        # ClientsController, AccountController, HomeController
├── Data/               # ApplicationDbContext, migrations, SeedData
├── Extensions/         # ClaimsPrincipalExtensions (current user helper)
├── Models/
│   ├── Entities/        # User, Client, PostalCode
│   └── ViewModels/      # Create/Edit/List/Details/Dashboard/Login/Register, ...
├── Services/            # IClientService/ClientService, IAuthService/AuthService
├── Views/               # Razor views (.cshtml)
├── wwwroot/
├── Program.cs
└── appsettings.json
tests/
└── ClientHub.Tests/     # xUnit — client service, authentication, ownership isolation
```

Domain model:

```text
PostalCode 1 ──── N Client
User       1 ──── N Client   (CreatedByUserId)
```

## Getting Started (development)

### Requirements

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- MySQL 8 (local install or Docker)

### Setup

```bash
git clone https://github.com/vvasconceloss/client-hub.git
cd client-hub
```

Start MySQL (Docker). The container exposes port **3307** on the host:

```bash
docker compose up -d
```

Restore and build:

```bash
dotnet restore
dotnet build
```

Configure the connection string with `dotnet user-secrets` (outside version control):

```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" \
  "Server=localhost;Port=3307;Database=clienthub;Uid=clienthub;Pwd=clienthub;AllowUserVariables=True"
```

Run the app. In Development, migrations and seed data are applied automatically on startup:

```bash
dotnet run
```

The app will be available at `http://localhost:5193`.

Alternatively, apply migrations and seed data manually:

```bash
dotnet ef database update
dotnet run
```

### Demo account

The seed data creates a demo user:

```text
Email:    demo@clienthub.dev
Password: ClientHub123!
```

You can also register your own account.

## Security

- Passwords are hashed with `PasswordHasher<User>` (PBKDF2, salted) — never custom hashing.
- Every POST action is protected with `[ValidateAntiForgeryToken]`.
- Client queries are scoped by the authenticated user on the server (`WHERE CreatedByUserId = ...`).
- ViewModels expose only editable fields, preventing overposting; the id comes from the route (`[FromRoute]`).
- Inputs are validated with Data Annotations before persistence.
- The connection string lives in `dotnet user-secrets` — nothing with credentials is committed.
- Production errors render generic pages with no stack traces.

## Testing

```bash
dotnet test
```

## Screenshots

> _Coming soon — dashboard, client list and form._

## License

This project is licensed under the MIT License — see the [LICENSE](LICENSE) file.