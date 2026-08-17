<div align="center">

# Client Hub

> A client management web app, rebuilt from scratch as a deliberate C#/.NET learning project — MVC,
> EF Core and MySQL, with authentication, per-user ownership and no shortcuts on security.

<p align="center">

[![License: MIT](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)
[![.NET](https://img.shields.io/badge/.NET-10-512bd4.svg)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-13-239120.svg)](https://learn.microsoft.com/en-us/dotnet/csharp/)
[![MySQL](https://img.shields.io/badge/MySQL-8-4479A1.svg)](https://www.mysql.com/)
[![Status](https://img.shields.io/badge/status-in%20development-yellow.svg)](docs/MVP.md)

</p>
</div>

---

## What is Client Hub?

Client Hub is a small ASP.NET Core MVC application for managing clients — the kind of internal tool
a small business would use to keep track of who they work with. A user registers, logs in, and
manages their own list of clients: creating, searching, filtering, sorting and editing records tied
to a postal code and to the user who owns them.

It's a **rebuild** of a project originally built in 2025 during my professional course.

It's built as a learning-first portfolio piece: MVC with Controllers and Razor Views (not an API),
Entity Framework Core against MySQL, cookie-based authentication and more.

---

## Project Status

No live deployment yet: Docker and hosting are explicitly out of scope for this MVP and may come in a later version.

> _Screenshots of the client list, forms and dashboard: coming soon

---

## Architecture

```
Browser
   │  HTML forms, cookies
   ▼
ASP.NET Core Middleware  (routing, authentication, error handling)
   │
   ▼
Controller
   │  binds request → ViewModel, checks ModelState
   ▼
Service        (IClientService, IAuthService — the only layer that talks to EF Core)
   │
   ▼
EF Core  (ApplicationDbContext)
   │
   ▼
MySQL
```

```
ClientHub/
│
├── Controllers/          # ClientsController, AccountController, HomeController
├── Data/                 # ApplicationDbContext, migrations
├── Models/
│   ├── Entities/          # User, Client, PostalCode
│   └── ViewModels/         # CreateClientViewModel, EditClientViewModel, LoginViewModel, ...
├── Services/              # IClientService/ClientService, IAuthService/AuthService
├── Views/                 # Razor views (.cshtml)
├── wwwroot/
│
├── Program.cs
└── appsettings.json
tests/
└── ClientHub.Tests/   # xUnit — Services, authentication, ownership isolation
```

Domain model:

```text
PostalCode 1 ──── N Client
User       1 ──── N Client   (CreatedByUserId)
```

---

## Getting Started (development)

### Requirements

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- MySQL 8 (local install or Docker)

### Setup

```bash
git clone https://github.com/vvasconceloss/client-hub.git
cd client-hub
```

Start MySQL (Docker):

```bash
docker compose up -d
```

Restore and build:

```bash
dotnet restore
dotnet build
```

Configure local secrets (outside version control):

```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" \
  "Server=localhost;Port=3306;Database=clienthub;User=clienthub;Password=clienthub" \
  --project src/ClientHub
```

Apply migrations and seed data:

```bash
dotnet ef database update --project src/ClientHub
```

Run the app:

```bash
dotnet run --project src/ClientHub
```

The app will be available at `http://localhost:5193`.

---

## Testing

```bash
dotnet test
```

---

## License

This project is licensed under the MIT License.