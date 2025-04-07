# Movie API 🎥

## Description 📋
This is a RESTful API built with .NET Core and Entity Framework Core that allows managing movies, categories, and users. It includes authentication and authorization via JWT and .NET Identity. This project is designed as a review of advanced ASP.NET Core concepts and best practices for building professional APIs.

---

## Features 🚀
- Full CRUD for **Movies**, **Categories**, and **Users**.
- Authentication and authorization handling using **JWT** and **.NET Identity**.
- Implementation of **Service Layer** for separated business logic.
- Global Middleware for handling custom errors.
- AutoMapper for mapping entities and DTOs.
- Swagger configured for automatic API documentation.
- Dependency configuration with Dependency Injection (DI).

---

## Technologies Used 🔧
- .NET Core 8.0
- Entity Framework Core
- AutoMapper
- Swagger
- JWT (JSON Web Tokens)
- .NET Identity
- SQL Server
- C# (Async/Await, Generics, Dependency Injection)

---

## Project Structure 📂
- **ApiPeliculas**: Main project containing all the API logic.
  - `Controllers/`: Route controllers.
  - `Services/`: Business logic organized in layers (Service Layer).
  - `Repositories/`: Data access with generic and specific Repositories.
  - `Extensions/`: Configuration of Swagger, JWT, and Identity separated from `Program.cs`.
  - `Entities/`: Models and DTOs used in the application.
  - `Migrations/`: Entity Framework Core migrations.

---

## Installation and Configuration 🔨
1. Clone the repository:
```bash
 git clone <Your-Repository-URL>
```

2. Restore NuGet packages:
```bash
 dotnet restore
```

3. Configure the database connection in `appsettings.json`:
```json
"ConnectionStrings": {
    "ConexionSql": "Server=(localdb)\\MSSQLLocalDB;Database=ApiPeliculasDB;Trusted_Connection=True;"
}
```

4. Apply Migrations:
```bash
 dotnet ef database update
```

5. Run the application:
```bash
 dotnet run
```

6. Access Swagger:
```
 https://localhost:5001/swagger/index.html
```

---

## Main Endpoints 📌
- **Categories:** Full CRUD.
- **Movies:** Full CRUD and advanced search.
- **Users:** Registration, Login, and JWT authentication.

---

## License 📜
MIT License

---

## Author 💼
Created by **Javier Adan Mendez Mendez** as part of an advanced .NET Core review.

