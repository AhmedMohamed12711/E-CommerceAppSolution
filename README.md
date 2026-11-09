# 🛒 E-Commerce API Solution (.NET 8 & Clean Architecture)

[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-12.0-239120?logo=csharp&logoColor=white)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![SQL Server](https://img.shields.io/badge/SQL%20Server-CC292B?logo=microsoftsqlserver&logoColor=white)](https://www.microsoft.com/sql-server)
[![Redis](https://img.shields.io/badge/Redis-DC382D?logo=redis&logoColor=white)](https://redis.io/)
[![Stripe](https://img.shields.io/badge/Stripe-635BFF?logo=stripe&logoColor=white)](https://stripe.com/)
[![JWT](https://img.shields.io/badge/JWT-000000?logo=jsonwebtokens&logoColor=white)](https://jwt.io/)
[![Swagger](https://img.shields.io/badge/Swagger-85EA2D?logo=swagger&logoColor=black)](https://swagger.io/)

A full-featured, scalable, and enterprise-grade E-Commerce Web API built with **ASP.NET Core 8**, following **Clean Architecture (Onion Architecture)** and Domain-Driven Design (DDD) principles. The solution incorporates **Redis** for basket storage and response caching, **Entity Framework Core** with **SQL Server** for persistent data, **ASP.NET Core Identity** with **JWT** for authentication and authorization, and **Stripe** for payment processing.

---

## 📑 Table of Contents

- [Architectural Overview](#-architectural-overview)
- [Key Features](#-key-features)
- [Tech Stack & Design Patterns](#-tech-stack--design-patterns)
- [Project Structure](#-project-structure)
- [API Endpoints Reference](#-api-endpoints-reference)
- [Database & Seed Data](#-database--seed-data)
- [Getting Started](#-getting-started)
  - [Prerequisites](#prerequisites)
  - [Configuration](#configuration)
  - [Run the Application](#run-the-application)
- [System Review & Recommendations](#-system-review--recommendations)

---

## 🏛 Architectural Overview

The application is architected following **Clean (Onion) Architecture**, ensuring strict separation of concerns, testability, and independence from external frameworks:

```
                      ┌──────────────────────────────────────┐
                      │          E-CommerceApp (API)         │
                      │   [Host / Middleware / Program.cs]   │
                      └──────────────────┬───────────────────┘
                                         │
                      ┌──────────────────▼───────────────────┐
                      │     Infrastructure / Presentation    │
                      │      [Controllers, Cache Filters]    │
                      └──────────────────┬───────────────────┘
                                         │
                      ┌──────────────────▼───────────────────┐
                      │            Core / Services           │
                      │  [Business Logic / Service Manager]  │
                      └──────────────────┬───────────────────┘
                                         │
                      ┌──────────────────▼───────────────────┐
                      │          Core / DomainLayer          │
                      │   [Entities, Contracts, Exceptions]  │
                      └──────────────────▲───────────────────┘
                                         │
                      ┌──────────────────┴───────────────────┐
                      │     Infrastructure / Persistence     │
                      │   [EF Core, Repositories, Migrations]│
                      └──────────────────────────────────────┘
```

---

## ✨ Key Features

- **Product Management:**
  - Full product catalog with brands and categories/types.
  - Advanced filtering by brand, type, and search keyword.
  - Dynamic sorting (price ascending/descending, name).
  - Server-side pagination with unified metadata (`PaginatedResult<T>`).
- **Shopping Basket (Redis):**
  - High-performance, fast in-memory basket management using Redis.
  - Real-time basket creation, item updates, delivery cost calculation, and deletion.
- **Order Processing:**
  - Complete checkout lifecycle: shipping address, delivery method selection, and order placement.
  - Automatic inventory price sync (prices validated from database, not client).
  - Order tracking and retrieval by user ID or GUID.
- **Payment Gateway (Stripe):**
  - End-to-end payment intent lifecycle via Stripe SDK.
  - Dynamic client-secret generation for secure client-side payment completion.
- **Authentication & Security:**
  - ASP.NET Core Identity with custom `ApplicationUser` and `Address` entities.
  - Secure password hashing and token-based authentication using **JWT Bearer**.
  - Role-based and claim-based access controls.
  - Cross-Origin Resource Sharing (CORS) configured for frontends (e.g., Angular/React).
- **Caching Layer:**
  - Custom Redis response caching attribute (`[Cash]`) for lightning-fast catalog responses.
- **Resilience & Developer Experience:**
  - Centralized global exception handler middleware (`CustomExceptionHandelerMiddleware`).
  - Standardized API validation response factory (`APIesponseFactory`).
  - Interactive OpenAPI / Swagger UI with persistent Bearer authorization.
  - Automatic database migration and mock data seeding on startup.

---

## 🛠 Tech Stack & Design Patterns

### Technologies
- **Framework:** .NET 8.0 (C# 12)
- **Data Persistence:** Entity Framework Core 8.0, SQL Server
- **In-Memory Cache & Storage:** StackExchange.Redis
- **Identity & Security:** ASP.NET Core Identity, System.IdentityModel.Tokens.Jwt
- **Payment Processor:** Stripe.net
- **Object Mapping:** AutoMapper
- **API Documentation:** Swashbuckle / Swagger UI

### Design Patterns & Principles
- **Clean Architecture / Onion Architecture**
- **Repository Pattern & Generic Repository**
- **Unit of Work Pattern**
- **Specification Pattern** (Dynamic query composition with expressions)
- **Service Manager / Factory Delegate Pattern** (Lazy loading services per request)
- **Dependency Injection (DI)**
- **Global Error Handling Middleware**

---

## 📂 Project Structure

```
E-CommerceAppSolution/
├── Core/
│   ├── DomainLayer/                   # Enterprise business entities & interfaces
│   │   ├── Constracts/                # Repository & UnitOfWork contracts
│   │   ├── Exceptions/                # Domain-specific custom exceptions
│   │   └── Models/                    # Domain models (Product, Order, Basket, Identity)
│   ├── Service/                       # Business logic implementations
│   │   ├── Specifications/            # Specification classes for dynamic queries
│   │   ├── MappingProfile.cs          # AutoMapper configurations
│   │   ├── ServiceManager.cs          # Aggregated service manager
│   │   └── *Service.cs                # Auth, Basket, Order, Payment, Product services
│   └── ServiceAbstraction/            # Service contracts & interfaces
├── Infrastructure/
│   ├── Presistance/                   # Persistence Layer
│   │   ├── Data/                      # StoreDbContext & StoreIdentityDbContext
│   │   │   ├── Config/                # Fluent API Entity configurations
│   │   │   ├── DataSeeding/           # Automatic JSON data seeders
│   │   │   └── Migrations/            # EF Core Migrations
│   │   └── Repository/                # Generic, Basket, Cash, UnitOfWork implementations
│   └── Presintation/                  # Presentation Layer
│       ├── Attributes/                # Custom action filters (Redis Cache)
│       └── Controllers/               # Web API Controllers (Products, Basket, Orders, Auth, Payment)
├── Shared/                            # Shared components across layers
│   ├── DTOS/                          # Data Transfer Objects
│   ├── Enums/                         # System enums
│   └── ErrorModel/                    # Standardized error response models
└── E-CommerceApp/                     # Web API Entry Point (Host)
    ├── CustomMiddleware/              # Global exception handling
    ├── Factories/                     # Custom model state error factories
    ├── wwwroot/                       # Static files (Product images)
    ├── appsettings.json               # Application configurations
    └── Program.cs                     # Startup and DI container configuration
```

---

## 🔌 API Endpoints Reference

### 🔐 Authentication (`/api/Authentication`)
| Method | Endpoint | Description | Auth Required |
|---|---|---|:---:|
| `POST` | `/api/Authentication/register` | Register a new user | No |
| `POST` | `/api/Authentication/login` | Authenticate and obtain JWT token | No |
| `GET` | `/api/Authentication/emailexists` | Check if email is already registered | No |
| `GET` | `/api/Authentication/CurrentUser` | Get current logged-in user details | Bearer JWT |
| `GET` | `/api/Authentication/address` | Get current user's shipping address | Bearer JWT |
| `PUT` | `/api/Authentication/address` | Update current user's shipping address | Bearer JWT |

### 🛍 Products & Catalog (`/api/Products`)
| Method | Endpoint | Description | Auth Required |
|---|---|---|:---:|
| `GET` | `/api/Products` | Get paginated products (Supports filter, sort, search, cached) | Bearer JWT |
| `GET` | `/api/Products/{id}` | Get product details by ID | No |
| `GET` | `/api/Products/Brands` | Retrieve all product brands | No |
| `GET` | `/api/Products/Types` | Retrieve all product types/categories | No |

### 🧺 Basket (`/api/basket`)
| Method | Endpoint | Description | Auth Required |
|---|---|---|:---:|
| `GET` | `/api/basket?key={id}` | Retrieve customer basket by key | No |
| `POST` | `/api/basket` | Create or update customer basket | No |
| `DELETE` | `/api/basket/{key}` | Clear/delete customer basket | No |

### 📦 Orders (`/api/orders`)
| Method | Endpoint | Description | Auth Required |
|---|---|---|:---:|
| `POST` | `/api/orders/CreateOrder` | Create a new order from basket | Bearer JWT |
| `GET` | `/api/orders/AllOrders` | Get all orders for current user | Bearer JWT |
| `GET` | `/api/orders/{id}` | Get order details by GUID | Bearer JWT |
| `GET` | `/api/orders/DeliveryMethods` | Get available delivery shipping methods | No |

### 💳 Payments (`/api/Payment`)
| Method | Endpoint | Description | Auth Required |
|---|---|---|:---:|
| `POST` | `/api/Payment/{basketId}` | Create or update Stripe PaymentIntent | Bearer JWT |

---

## 💾 Database & Seed Data

On application startup, `DataSeeding.cs` automatically verifies and populates default data:
- **Product Brands:** Sample brands loaded from `brands.json`
- **Product Types:** Categories loaded from `types.json`
- **Products:** Detailed catalog items with image URLs loaded from `products.json`
- **Delivery Methods:** Standard, Express, and Next-Day options from `delivery.json`
- **Default Identity Users:** Seeded administrative and test accounts

---

## 🚀 Getting Started

### Prerequisites

Ensure you have the following installed on your machine:
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/sql-server) or SQL LocalDB
- [Redis](https://redis.io/download/) (Local service or Docker container)

### Configuration

Update your connection strings and API keys in `E-CommerceApp/appsettings.json` (or use .NET User Secrets):

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;DataBase=ECommerceAPP;Trusted_Connection=True;TrustServerCertificate=True;",
    "IdentityConnection": "Server=.;DataBase=ECommerceAPP.Identity;Trusted_Connection=True;TrustServerCertificate=True;",
    "RediusConnection": "localhost"
  },
  "JWTOptions": {
    "Secretkey": "<YourSuperSecretLongKeyHere>",
    "Issuer": "https://localhost:11234/",
    "Audience": "https://localhost:7112/api"
  },
  "StripSection": {
    "SecretKey": "sk_test_...",
    "EndPointSecret": "whsec_..."
  }
}
```

> 💡 **Tip:** If running Redis with Docker, execute:
> ```bash
> docker run -d --name redis-server -p 6379:6379 redis
> ```

### Run the Application

1. **Clone the repository:**
   ```bash
   git clone https://github.com/AhmedMohamed12711/E-CommerceAppSolution.git
   cd E-CommerceAppSolution
   ```

2. **Restore dependencies:**
   ```bash
   dotnet restore
   ```

3. **Build the solution:**
   ```bash
   dotnet build
   ```

4. **Run the API:**
   ```bash
   cd E-CommerceApp
   dotnet run
   ```

5. **Access Swagger UI:**
   Navigate to `https://localhost:7112/swagger` in your browser to explore and test the endpoints.

---

## 🔍 System Review & Recommendations

During our codebase review, the following strengths and suggested enhancements were identified:

### ✅ Strengths
1. **Clean Separation of Concerns:** Onion Architecture separates domain models, service business logic, and presentation controllers cleanly.
2. **Specification Pattern:** Flexible querying mechanism enabling modular sorting, filtering, and eager loading without repository bloat.
3. **High-Performance Caching:** Direct Redis caching significantly reduces SQL Server load for read-heavy product catalog operations.
4. **Resilient Error Responses:** Custom global middleware ensures uniform error formatting across the application.

### 💡 Recommended Improvements & Fixes
1. **`PaymentController` Async Bug:**
   In `PaymentController.CreateOrUpdatePaymentIntentAsync`, `_serviceManager.paymentService.CreateOrUpdatePaymentAsync(BasketId)` is not awaited. Adding `await` prevents returning an uncompleted `Task` directly in `Ok()`.
2. **Naming & Folder Typo Refactoring:**
   - Folder `Presintation` ➔ `Presentation`
   - Folder `Presistance` ➔ `Persistence`
   - Folder `Constracts` ➔ `Contracts`
   - Service & Interface `CashService` / `ICashService` ➔ `CacheService` / `ICacheService` (Cash refers to money, Cache refers to in-memory store)
   - Interface `IBaskerRepository` ➔ `IBasketRepository`
   - Setting `RediusConnection` ➔ `RedisConnection`
   - Setting `StripSection` ➔ `StripeSection`
3. **Security (Secret Keys):**
   The Stripe secret key and JWT key are committed in `appsettings.json`. For production safety, move these to environment variables or **Azure Key Vault** / **.NET User Secrets** (`dotnet user-secrets`).
4. **Stripe Webhook Completion:**
   Implement and enable the commented Stripe webhook handler in `PaymentService` to automatically transition orders to `PaymentReceived` or `PaymentFailed` upon receiving Stripe events asynchronously.
5. **AutoMapper Advisory:**
   Upgrade/review AutoMapper version to resolve package vulnerability warning `NU1903`.

---

## 📄 License
This project is licensed under the MIT License - feel free to use and modify for your personal or commercial projects.
