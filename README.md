🌩️ CloudForge LMS

CloudForge LMS is a cloud‑native Learning Management System (LMS) designed to deliver AWS, DevOps, and Cloud Engineering training at scale.

This project is built as a real SaaS‑grade backend + frontend system, not a demo app. It follows clean architecture, CQRS, JWT authentication, MongoDB (Azure Cosmos DB – Mongo API), and Angular for the frontend.

Tagline: Where cloud engineers are forged.

🚀 Key Features
👤 Authentication & Users

JWT‑based authentication

Secure password hashing (BCrypt)

Role‑based access (student, trainer, admin)

Profile update via JWT claims (email‑based)

📚 Courses & Content

Course catalog (published / draft)

Course metadata (level, thumbnail, creator)

Modular course structure (Modules → Lessons)

Trainer/Admin course management

🎬 Video Progress Tracking

Resume playback support

Watched duration tracking

Auto‑completion logic

Idempotent UPSERT design

⚙️ Platform Architecture

Clean Architecture (API / ApplicationCore / Infrastructure)

Repository Pattern (selective)

CQRS (Read / Write separation)

Centralized logging & caching

Azure SDK integration (Blob Storage ready)

🧱 Architecture Overview
Angular (Frontend)
   ↓ REST APIs
ASP.NET Core 8 Web API
   ↓
ApplicationCore (DTOs, Interfaces, Commands, Queries)
   ↓
Infrastructure (Services, Repositories, MongoDB, Azure SDK)
   ↓
Azure Cosmos DB (Mongo API)
🛠️ Tech Stack
Backend

.NET 8 – ASP.NET Core Web API

MongoDB (Azure Cosmos DB – Mongo API)

JWT Authentication

CQRS (Command / Query split)

Repository Pattern (where applicable)

xUnit + Moq (Unit Testing)

Frontend

Angular (SPA)

JWT‑based auth integration

Cloud & DevOps Ready

Azure SDK (Blob Storage, future extensions)

Cloud‑native configuration & logging

📂 Solution Structure
CloudForge.sln
│
├── API
│   ├── Controllers
│   ├── Program.cs
│   └── API.csproj
│
├── ApplicationCore
│   ├── Model
│   ├── DTOs
│   ├── Interfaces
│   ├── Commands
│   └── Queries
│
├── Infrastructure
│   ├── Data
│   ├── Services
│   ├── Repositories
│   └── Azure
│
└── Tests
    ├── Services
    ├── Helpers
    └── Tests.csproj
🔐 Authentication Flow

User registers

User logs in → receives JWT

JWT stored in client

Protected APIs accessed using Authorization: Bearer <token>

Email & role extracted from token claims

📦 Example API Endpoints
Auth

POST /api/auth/register

POST /api/auth/login

PUT /api/auth/me

Courses

GET /api/courses (public)

POST /api/courses (trainer/admin)

PUT /api/courses/{id}

Video Progress

POST /api/video-progress

GET /api/video-progress/{lessonId}

🧪 Testing Strategy

Unit tests in separate Tests project

Services tested using Moq

MongoDB isolated via Repository abstraction

No database dependency for unit tests

Run tests:

dotnet test
🔍 Design Principles

Separation of concerns

Business logic isolated from persistence

Minimal abstractions (no over‑engineering)

Scalable read/write patterns (CQRS)

Cloud‑ready by default

🌱 Future Enhancements

Course enrollment & completion analytics

Search & pagination

Distributed cache (Redis)

Event‑driven progress updates

CI/CD pipelines

Observability (OpenTelemetry)

👨‍💻 Author

CloudForge LMS is built as a real‑world cloud education platform, designed for learners, trainers, and enterprises.

This project showcases production‑grade backend engineering, not tutorial‑level code.

📜 License

MIT License
