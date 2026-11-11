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
<img width="1903" height="942" alt="endpoints" src="https://github.com/user-attachments/assets/c3e2972d-4180-412f-9920-ab710f762851" />

### Unit Tests
<img width="995" height="487" alt="Screenshot 2026-10-15 212157" src="https://github.com/user-attachments/assets/c0f4bea9-4541-46cf-a67c-3179b353e7cb" />
![Uploading unit test.png…]()

### CI/CD — GitHub Actions


RaceDay Web Application — Part 3

Project Overview

RaceDay is a complete event management system built with ASP.NET Core MVC for the frontend and ASP.NET Core Web API for the backend. This is Part 3 of the project, covering the web application interface, user authentication, and role-based dashboards.

Technologies Used

• Frontend: ASP.NET Core MVC, Razor Views, Bootstrap

• Backend: ASP.NET Core Web API, RESTful endpoints

• Authentication: Cookie-based authentication

• Database: SQL Server with Entity Framework Core

• Architecture: Two-project solution — RaceDay.Api and RaceDay.Web

Features

User Authentication

• Register — Create an account as Participant or Organiser

• Login — Sign in with email and password

• Logout — End your session

• Protected pages — Dashboards require login

Participant Dashboard

• View upcoming events

• Register for races

• View race history

Organiser Dashboard

• Create new events

• View registered participants

• Manage race information

How to Run

1. Open the solution in Visual Studio

2. Set up the database:

◦ Open Package Manager Console

◦ Run: Update-Database -Project RaceDay.Api

3. Start the API first — press F5 on RaceDay.Api

4. Then start the Web app — press F5 on RaceDay.Web

5. Go to http://localhost:5000 in your browser

Project Structure

• RaceDay.Api — Backend services and database

• RaceDay.Web — Frontend website and user interface


