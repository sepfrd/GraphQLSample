# GraphQLSample

A sample .NET web application demonstrating GraphQL with HotChocolate, using Clean Architecture practices, integrating CQRS, the Mediator pattern, MongoDB, and JWT-based authentication and authorization.

## 🧱 Architecture Overview

- **Clean Architecture**: Separation of concerns with distinct layers for Domain, Application, Infrastructure, and Presentation.
- **CQRS (Command Query Responsibility Segregation)**: Separates read and write operations for better scalability and maintainability.
- **Mediator Pattern**: Decouples the sender and receiver, promoting loose coupling and single responsibility.
- **MongoDB**: Serves as the primary NoSQL database.
- **GraphQL with HotChocolate**: Provides a flexible and efficient API layer.
- **JWT Authentication**: Implements secure authentication and authorization mechanisms.

## 🚀 Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Docker](https://www.docker.com/)

### Installation

1. **Clone the repository**:

   ```bash
   git clone https://github.com/sepfrd/GraphQLSample.git
   cd GraphQLSample
   ```

2. **Start the application using Docker Compose**:

   ```bash
   MONGO_ROOT_USERNAME=mongodb_username \
   MONGO_ROOT_PASSWORD=mongodb_password \
   ENABLE_DATA_SEED=false \
   docker compose --file docker-compose.yaml up --detach
   ```

## 🔍 Project Structure

```
GraphQLSample/
├── src/
│   ├── Application/        # Application layer (CQRS handlers, interfaces)
│   ├── Domain/             # Domain entities and interfaces
│   ├── Infrastructure/     # Data access, external services
│   └── Web/                # API controllers, GraphQL setup
├── docker-compose.yaml
├── GraphQLSample.sln
└── README.md
```

## 🛠️ Technologies Used

- **.NET 9**
- **MongoDB**
- **GraphQL (HotChocolate)**
- **MediatR**
- **JWT Authentication**
- **Docker**
