# 🛡️ Event-Driven Audit System

An ASP.NET Core Web API that tracks user actions asynchronously using **Clean Architecture**, **CQRS**, and an **Event-Driven** background processing mechanism.

---

## ✨ Key Features

- **Event-Driven Audit Logging** — every action (Login, Register, Enroll, Create/Update/Delete) fires an `AuditEvent` that is processed in the background **without blocking the API response**
- **CQRS Pattern** — Commands and Queries are fully separated via MediatR
- **JWT Authentication** — secure token-based auth with Role-Based Authorization (Admin / User)
- **Password Hashing** — all passwords are hashed using BCrypt
- **Unit of Work Pattern** — all DB operations coordinated through a single `SaveChangesAsync`
- **Input Validation** — all requests validated using FluentValidation
- **Data Mapping:** — Configured AutoMapper for clean object transformation.
- **Generic Response Wrapper** — all endpoints return a consistent `RequestResponse<T>` shape


---

## ⚙️ How the Audit System Works
1. **Action Trigger:** When a user performs a tracked action (e.g., executing the Command to Enroll in a Course), an Audit Event is generated.
2. **In-Memory Queuing:** The event is pushed into a thread-safe, in-memory queue.
3. **Immediate Response:** The API immediately returns a response to the client without waiting for the database write.
4. **Background Service:** A `BackgroundService` listens to the queue, dequeues the event, and asynchronously saves the audit record (including `UserId`, `Action` like "EnrollCourse", `EntityName`, `EntityId`, and `Timestamp`) to the database.


---

## 🔐 How to Login

**1. Register:**
```http
POST /api/auth/register
Content-Type: application/json

{
  "name": "John Doe",
  "email": "john@example.com",
  "password": "John@123"
}
```

**2. Login:**
```http
POST /api/auth/login
Content-Type: application/json

{
  "email": "john@example.com",
  "password": "John@123"
}
```

**Response:**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "email": "john@example.com"
}
```

**3. Use the token in all requests:**
```http
Authorization: Bearer {your_token}
```

> In Swagger UI, click 🔒 **Authorize** and enter: `Bearer {your_token}`

### Seeded Admin Account
```
Email:    admin@gmail.com
Password: Admin@123
```

---

## 🚀 How to Run Locally

**1. Clone the repository:**
```bash
git clone https://github.com/RauthIsaac/AuditSystem.git
cd AuditSystem
```

**2. Update `appsettings.json`:**
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=AuditSystemDb;Trusted_Connection=True;TrustServerCertificate=True"
},
"JwtSettings": {
  "Secret": "your-super-secret-key-at-least-32-characters"
}
```

**3. Apply migrations:**
```bash
cd AuditSystem.API
dotnet ef database update
```

**4. Run:**
```bash
dotnet run
```

**5. Open Swagger:** `https://localhost:{port}/swagger`

---

