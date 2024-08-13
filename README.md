# JaiminPatelECommerce

JaiminPatelECommerce is a simple e-commerce application built with ASP.NET Core, Entity Framework Core, and Identity for user management. This project serves as an example of a fully functional web application with features like product management, user authentication, and role-based access control.

## Features

- User registration and authentication
- Role-based access control (Admin and Customer roles)
- Product listing, adding to cart, and checkout process
- Admin dashboard for managing products and orders

## Prerequisites

Before you begin, ensure you have the following installed on your machine:

- [.NET SDK 8.0 or later](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (LocalDB or another SQL Server instance)
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) or [Visual Studio Code](https://code.visualstudio.com/)

## Setup Instructions

Follow these steps to set up and run the project on your local machine.

### 1. Clone the Repository

```bash
git clone https://github.com/jaiminpatel1999/JaiminPatelECommerce.git
cd JaiminPatelECommerce
```

### 2. Configure the Database Connection

1. Open the `appsettings.json` file located in the root of the project.
2. Modify the `ConnectionStrings` section to point to your SQL Server instance. The default configuration is for LocalDB:

    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=JaiminPatelECommerceDb;Trusted_Connection=True;MultipleActiveResultSets=true"
    }
    ```

3. If you're using SQL Server Authentication, update the connection string accordingly:

    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Server=your_server_name;Database=JaiminPatelECommerceDb;User ID=your_username;Password=your_password;MultipleActiveResultSets=true"
    }
    ```

### 3. Apply Migrations and Seed the Database

Run the following commands in the terminal to apply the database migrations and seed the initial data:

```bash
dotnet ef database update
```

### 4. Run the Application

To run the application, use the following command:

```bash
dotnet run
```

Alternatively, you can run the application from Visual Studio by pressing `F5` or selecting `Debug > Start Debugging`.

### 5. Access the Application

Once the application is running, you can access it by navigating to `https://localhost:5001` or `http://localhost:5000` in your web browser.

### 6. Login Credentials

After seeding the database, you can log in as an Admin using the following credentials:

- **Email:** `admin@ecommerce.com`
- **Password:** `Admin@123`

### 7. Admin Features

As an Admin, you can access the Admin Dashboard where you can manage products and orders. This link will only be visible if you are logged in as a user with the `Admin` role.

## Project Structure

- **Controllers/**: Contains the application controllers responsible for handling user input and returning responses.
- **Models/**: Contains the entity models and view models used throughout the application.
- **Views/**: Contains the Razor views that define the UI.
- **Data/**: Contains the `ApplicationDbContext` class responsible for interacting with the database.
- **Services/**: Contains the service classes that encapsulate business logic and data access.

## Technologies Used

- ASP.NET Core MVC
- Entity Framework Core
- ASP.NET Core Identity
- Bootstrap (for responsive UI)
- SQL Server (LocalDB)

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

## Troubleshooting

If you encounter any issues, please check the following:

- Ensure your database connection string is correctly configured in `appsettings.json`.
- Make sure you have applied the latest migrations using `dotnet ef database update`.
- If you encounter any issues with package dependencies, try running `dotnet restore` to resolve them.
