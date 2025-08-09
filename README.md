# E-Commerce API

A feature-rich, secure RESTful API built with ASP.NET Core that provides the backend functionality for a modern e-commerce platform.

This project demonstrates a professional, enterprise-grade application structure. It includes a complete user authentication and authorization system using JWTs, role-based security for administrative tasks, and product catalog management.

---

## Features

-   **Secure Authentication:** Full user registration and login system built with ASP.NET Core Identity.
-   **JWT-Based Security:** Uses JSON Web Tokens (JWTs) for authenticating API requests, ensuring stateless and secure communication.
-   **Role-Based Authorization:** Implements a robust role system ("Admin", "Customer") to protect sensitive endpoints.
-   **Automated Admin Creation:** The first user who registers is automatically assigned the "Admin" role, simplifying setup.
-   **Product Catalog Management:** Full CRUD (Create, Read, Update, Delete) functionality for products, with administrative actions protected.
-   **Clean Architecture:** Follows professional best practices by separating concerns, with logic moved into dedicated services and configuration extensions.
-   **Swagger Documentation:** Includes a complete and interactive Swagger UI for easy exploration and testing of all API endpoints.

---

## Technologies Used

-   **Backend:** C#, ASP.NET Core 8
-   **Database:** Entity Framework Core with Azure SQL
-   **Security:** ASP.NET Core Identity, JWT (JSON Web Tokens)
-   **API Documentation:** Swashbuckle (Swagger)
-   **Deployment:** Microsoft Azure App Service

---

## Getting Started

### Prerequisites

-   [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
-   An SQL Server instance (local, Docker, or a cloud provider like Azure).
-   A code editor like Visual Studio or VS Code.

### Setup and Installation

1.  **Clone the repository:**
    ```bash
    git clone [Your GitHub Repository URL]
    ```

2.  **Navigate to the project directory:**
    ```bash
    cd ECommerceAPI
    ```

3.  **Configure your connection string:**
    -   Open `appsettings.json`.
    -   Find the `ConnectionStrings` section and replace the value of `DefaultConnection` with the connection string for your own SQL Server instance.

4.  **Restore dependencies:**
    ```bash
    dotnet restore
    ```

5.  **Run the database migrations:**
    This command will connect to your database and create all the necessary tables for Identity and Products.
    ```bash
    dotnet ef database update
    ```

6.  **Run the application:**
    ```bash
    dotnet run
    ```
    The API will be available at `https://localhost:7123` (or a similar port). You can access the interactive Swagger documentation by navigating to `https://localhost:7123/swagger`.

---

## Security and Usage Flow

1.  **Register Your First User:** Make a `POST` request to `/api/user/register`. This first user will automatically be granted the "Admin" role.
2.  **Log In:** Make a `POST` request to `/api/user/login` with your new user's credentials to receive a JWT.
3.  **Use the Token:** Copy the received JWT. In Swagger, click the "Authorize" button and paste the token in the format `Bearer [your_token]`. For other tools like Postman, add an `Authorization` header with the same value.
4.  **Access Protected Endpoints:** You can now make requests to endpoints that require authentication, such as creating a product.

---

## API Endpoints

### Accounts

-   **`POST /api/user/register`**
    -   **Description:** Registers a new user. The first user registered is automatically made an Admin. All subsequent users are assigned the "Customer" role.
    -   **Request Body:** `{"userName": "michael", "email": "michael@example.com", "password": "Password123!"}`

-   **`POST /api/user/login`**
    -   **Description:** Authenticates a user and returns a JWT.
    -   **Request Body:** `{"userName": "michael", "password": "Password123!"}`

### Products

-   **`GET /api/products`**
    -   **Description:** Retrieves a list of all products.
    -   **Auth:** None required.

-   **`GET /api/products/{id}`**
    -   **Description:** Retrieves a single product by its ID.
    -   **Auth:** None required.

-   **`POST /api/products`**
    -   **Description:** Creates a new product.
    -   **Auth:** **Admin** role required.
    -   **Request Body:** `{"name": "Laptop", "description": "A powerful laptop", "price": 1200.00, "stockQuantity": 50}`

-   **`PUT /api/products/{id}`**
    -   **Description:** Updates an existing product.
    -   **Auth:** **Admin** role required.

-   **`DELETE /api/products/{id}`**
    -   **Description:** Deletes a product.
    -   **Auth:** **Admin** role required.
