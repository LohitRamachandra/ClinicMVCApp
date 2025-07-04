# ClinicMVCApp

🏥 Patient Management System – dtic Clinic Centre
A full-stack ASP.NET Core MVC application designed for managing patients and appointment bookings. Built as part of a technical assessment, this project demonstrates clean architecture, form validation, and responsive UI design.

📦 Technologies Used
- ASP.NET Core MVC 8+
- Entity Framework Core
- SQL Server
- Razor Views + Bootstrap
- LINQ and Repository Pattern

⚙️ Setup Instructions
- Clone the Repo
git clone https://github.com/LohitRamachandra/ClinicMVCApp.git


- Update appsettings.json
Add your SQL Server connection string:
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=ClinicDb;Trusted_Connection=True;"
}


- Apply Migrations
dotnet ef database update


- Run the Application
dotnet run


Then navigate to https://localhost:xxxx in your browser.
