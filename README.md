# ShoppingMasterApp

ShoppingMasterApp is a full-featured e-commerce platform designed to deliver a smooth and scalable shopping experience. It is built with .NET Core on the backend and uses a modern frontend framework (React or similar) to provide a responsive user interface. The application follows a CQRS (Command Query Responsibility Segregation) architecture to ensure a clean separation of commands and queries, making the system highly maintainable and extensible.

---

## Key Features

- **User Management**: Role-based access control with JWT-based authentication. Supports both customer and admin roles with secure email verification.
- **Product Management**: Complete CRUD operations for managing products with support for product categories, stock management, and SKU handling.
- **Cart & Order Management**: Allows customers to add products to their cart, modify items, and process orders with integrated payment gateways.
- **Discount & Review System**: Supports discount codes for promotions and a review system for customers to leave feedback on products.
- **Middleware & Filters**: Custom middleware for exception handling, request-response logging, and input validation.
- **Graylog Integration**: Logs are structured and sent to Graylog for centralized logging, making it easier to monitor application behavior, track errors, and analyze system performance.
- **Frontend**: A modern, responsive user interface (React/Angular/Vue), providing users with a smooth shopping experience.

---

## Technology Stack

- **Backend**: .NET Core 8, MediatR for CQRS, Entity Framework Core for data access
- **Frontend**: React design
- **Database**: SQL Server
- **Authentication**: JWT (JSON Web Token) for secure user authentication
- **Logging**: Integrated with Graylog and Serilog for advanced logging and error tracking
- **API Documentation**: Swagger UI for interactive API testing and documentation

---

## Getting Started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) (version 8.0)
- Node.js and npm (if you are working with the frontend)
- SQL Server

### Installation

1. **Clone the repository:**
   ```bash
   git clone https://github.com/firatkaanbitmez/ShoppingMasterApp.git
   cd ShoppingMasterApp
   ```

2. **Backend Setup:**
   - Update the `appsettings.json` file with your SQL Server connection string.
   - Run the following command to apply the database migrations:
     ```bash
     dotnet ef database update
     ```
   - Start the backend:
     ```bash
     dotnet run
     ```

3. **Frontend Setup (if applicable):**
   - Navigate to the frontend directory (if it's a separate project):
     ```bash
     cd ShoppingMasterApp.Frontend
     ```
   - Install the required npm dependencies:
     ```bash
     npm install
     ```
   - Start the frontend:
     ```bash
     npm start
     ```

4. **Graylog Configuration:**
   - Ensure your Graylog server is running and properly configured to receive logs.
   - Update your logging configuration in `appsettings.json` to point to your Graylog instance:
     ```json
     "Serilog": {
       "MinimumLevel": "Information",
       "WriteTo": [
         {
           "Name": "Graylog",
           "Args": {
             "hostnameOrAddress": "your-graylog-server-address",
             "port": 12201
           }
         }
       ]
     }
     ```

5. **Access API and Frontend:**
   - The backend will be accessible at `http://localhost:5000/swagger` for API testing.
   - The frontend will be available at `http://localhost:3000`.

---

## Architecture Overview

- **CQRS**: The application is designed with CQRS to separate the command (write) and query (read) operations. This improves scalability and maintainability.
- **MediatR**: Used for handling commands and queries, promoting clean code by decoupling request handling from controllers.
- **Entity Framework Core**: For data access and database interactions.
- **Graylog**: Centralized logging system, integrated via Serilog, to provide structured logging, error tracking, and performance analysis across the application.

---

## Logging and Monitoring

- **Graylog**: Logs are sent to a central Graylog server, allowing detailed monitoring of application behavior. It provides insights into system health, helps in identifying bottlenecks, and tracks errors for debugging purposes.
- **Serilog**: Structured logging is implemented, with logs being pushed to Graylog in a Gelf format, ensuring efficient and clear log data.

---

## Contribution

We welcome contributions! Feel free to fork the project, make your changes, and submit a pull request.

---

## License

This project is licensed under the MIT License.
