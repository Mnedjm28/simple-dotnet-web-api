# Simple .NET Web API 🚀

This is a cleanly structured ASP.NET Core Web API project built to demonstrate real-world backend development skills, including:

✔ Clean Architecture  
✔ CQRS (with MediatR)  
✔ Repository Pattern  
✔ JWT Authentication  
✔ FluentValidation  
✔ AutoMapper  
✔ File Upload  
✔ Modular Startup Organization  
✔ EF Core Configurations & Relationships  
✔ Middleware & Filters  
✔ Custom Authorization Policies

---

## 🏗 Architecture

The solution follows a simplified Clean Architecture approach:

- **Domain** → Entities only
- **Application** → Business logic (CQRS, validators, DTOs)
- **Infrastructure** → Authentication, filters, middleware, utilities
- **Persistence** → EF Core + Repositories
- **API Layer** → Controllers, middleware, configuration

---

## 🔍 Highlights

### 🔐 Authentication & Authorization

- JWT Bearer Authentication
- Role-based authorization
- Custom Permission-based Authorization (via attributes + handler)
- Configurable token settings

### 🧰 Middleware & Filters

- **Middleware**: Handles cross-cutting concerns like exception handling, logging, and request validation globally.
- **Filters**: Apply pre- or post-processing logic to actions, including model validation, action logging, and result formatting.

### 📦 File Upload System

Supports image uploading using `IFormFile`. Images are converted to byte arrays and stored directly in the database.

### ⚡ Categories Module (CQRS)

- Commands
- Queries
- Handlers
- Validators
- MediatR pipeline behaviors

Ensures clear separation between read and write operations.

### 📚 Items & Users (Repository Pattern)

- CRUD operations
- AutoMapper DTO mapping
- Optional image upload

### 💾 EF Core Features

- Entity relationships configured via Fluent API (One-to-Many, Many-to-Many)
- Key constraints and precision for numeric columns
- Seeded data for roles and permissions
- Clean separation of persistence concerns into `Persistence` layer

### 📝 Policies

- Fine-grained authorization using **custom policies**
- Policies check permissions dynamically based on roles and assigned user permissions
- Easy to extend with additional requirements

---

## 🧰 Tech Stack

- **.NET 8**
- **Entity Framework Core**
- **MediatR (CQRS)**
- **AutoMapper**
- **FluentValidation**
- **SQL Server**
- **JWT Authentication**
- **Swagger (OpenAPI)**

---

## 🧩 Modules Overview

### **Categories (CQRS)**

- Clean separation of read / write responsibilities
- Full command & query validation
- Easy to extend

### **Items (Repository Pattern + File Upload)**

- CRUD operations
- Supports uploading images using multipart-form
- AutoMapper integrated

### **Users**

- Basic CRUD
- Username and email uniqueness checking
- Role and permission management

### **Config**

- Returns current runtime configuration settings
- Useful for debugging and environment checks

---

## 🚀 Run the App

### 1️⃣ Restore packages

```bash
dotnet restore
```

### 2️⃣ Run the project

```bash
dotnet run
```

### 3️⃣ Access Swagger UI

https://localhost:5001/swagger  
http://localhost:5000/swagger

---

## 🧱 Clean Architecture Folder Layout

```
Domain
Application
Infrastructure
Persistence
simple-dotnet-web-api
```

Each layer is independent and cleanly separated, ensuring a scalable and maintainable structure.

---

## ✨ Author

This project was built to practice, learn, and demonstrate professional backend development using ASP.NET Core.

Feel free to explore, clone, or extend it!
