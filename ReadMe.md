# BookNest – Library Management System (ASP.NET MVC)

BookNest is a simple ASP.NET MVC web application designed to manage a
library’s books, members, librarians, and book checkouts.  
This project was built to demonstrate clean MVC architecture, Entity
Framework Core, and role-based authentication and authorization.

<img width="1863" height="842" alt="allCheckouts" src="https://github.com/user-attachments/assets/db6777e2-2406-4b93-8c00-2614e0034d0f" />

---

## Tech Stack

- ASP.NET MVC 
- Entity Framework Core
- ASP.NET Core Identity
- SQL Server / LocalDB
- Bootstrap 5

---

## User Roles

The application uses three predefined roles:

- **Admin**
  - Can manage librarians
- **Librarian**
  - Manage books and authors
  - View and manage checkouts
  - Can return books
- **Member**
  - View available books
  - Checkout books

---

## Seeded Demo Users

The following users are seeded automatically on first run for testing:

| Role       | Email                  | Password  |
|------------|------------------------|-----------|
| Admin      | admin@booknest.com     | P@ssw0rd  |
| Librarian | librarian@booknest.com | P@ssw0rd  |
| Member    | member@booknest.com    | P@ssw0rd  |

---
<img width="1870" height="936" alt="ActiveMemberCheckout" src="https://github.com/user-attachments/assets/273c97f3-12af-49b8-b7ad-a386f6f7e11a" />

<img width="1867" height="461" alt="overdueCheckout" src="https://github.com/user-attachments/assets/077ed1ee-3d0d-4786-8608-7acbbf50f4ca" />

## How to Run the Project

### Prerequisites
- Visual Studio 2022 or newer / VS Code
- .NET SDK10 or newer
- EF Core CLI tools
- SQL Server or SQL Server Express


### Steps
1. Clone the repository

2. Open the solution  
Open `BookNest.sln` in Visual Studio / VS Code.

3. Configure the database  
Update the connection string in `appsettings.json`.

4.  Run migrations and update the database
   
5.  Run the application

