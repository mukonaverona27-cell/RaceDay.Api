# RaceDay.Api
RaceDay API

A RESTful API built with ASP.NET Core for managing race event registrations, results, and categories.

 Setup & Run Instructions

Prerequisites: .NET 6.0+, SQL Server, Visual Studio 2022+

Steps:

1. Clone: git clone https://github.com/mukonaverona27-cell/RaceDay.Api.git

2. Update connection string in appsettings.json to match your SQL Server

3. In Package Manager Console: Update-Database

4. Press F5 to run — Swagger UI at https://localhost:xxxx/swagger

User Roles

 Organiser: Create/update/delete races, categories, participants; view all data — full access

 Participant: View races, register, view own results — limited access

 Project Structure

• Controllers/ — API endpoints


• Models/ — Database models & DTOs

• Data/ — Database context

• Program.cs — App entry point

• appsettings.json — Configuration

Authentication

Uses JWT Bearer Tokens. Register: /api/auth/register — Login: /api/auth/login — Include token in header: Authorization: Bearer {your-token}

Unit Tests

Test project: RaceDay.Api.Tests — Run in Visual Studio Test Explorer — 2 Tests Passed ✅

 CI/CD — GitHub Actions

Workflow file: .github/workflows/ci.yml — Automatically builds and tests on every commit.
## 📸 Screenshots

### Swagger UI
<img width="995" height="487" alt="unit test" src="https://github.com/user-attachments/assets/7787760d-9c83-4f51-9c64-279315c616e0" />
<img width="1903" height="942" alt="endpoints" src="https://github.com/user-attachments/assets/a069345b-1101-4b38-b57b-76b81529d10e" />
<img width="1903" height="942" alt="endpoints" src="https://github.com/user-attachments/assets/c3e2972d-4180-412f-9920-ab710f762851" />

### Unit Tests
<img width="995" height="487" alt="Screenshot 2026-10-15 212157" src="https://github.com/user-attachments/assets/c0f4bea9-4541-46cf-a67c-3179b353e7cb" />
![Uploading unit test.png…]()

### CI/CD — GitHub Actions
*(Paste your ci.yml file screenshot here)*

## 🎥 Video Demonstration
https://www.youtube.com/watch?v=your-video-code-here

(Screenshot of CI/CD pipeline goes here)

 Video Demonstration

(Paste your YouTube video link here)
Thank you for reviewing my submission! ✨
