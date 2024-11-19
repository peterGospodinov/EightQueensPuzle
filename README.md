## Overview

This project provides a solution to the classic N Queens puzzle, where the challenge is to place N queens on an NxN chessboard such that no two queens threaten each other. The resulting solutions are stored in a Microsoft SQL Server database and can be accessed via an API. The project aims to efficiently solve the N Queens puzzle using parallel computation and advanced solution normalization techniques to identify fundamental solutions.

- **Solution Producer and Consumer**: Implements producer-consumer logic to generate and process solutions asynchronously.
- **Fundamental Solutions Normalization**: Uses a normalization process to identify unique, fundamental solutions among all possible configurations.
- **Parallel Computation**: Uses `Parallel.ForEach` to process multiple solutions concurrently for improved efficiency.
- **BlockingCollection**: Utilizes `BlockingCollection` for thread-safe communication between solution producers and consumers.
- **EF Core and Database Persistence**: The results of the computation are saved into an SQL database using Entity Framework Core. The project includes database migrations, which are applied automatically after the first start.
- **API Access**: Provides an API to access stored solutions, enabling easy integration with other systems.
- **Console Application for Displaying Results:**: Includes a small console application to quickly display the computed results.

## Tech Stack

- **Backend**: C#, .NET 8
- **Database**: MSSQL (mssql/server:2022-latest)
- **Database Access**: Microsoft.EntityFrameworkCore
- **Tools and Libraries**: Swashbuckle for API documentation, Newtonsoft.Json for serialization

### API Endpoints

- **Get Results**: [http://localhost:8080/api/Results](http://localhost:8080/api/Results)
- **Get Result by ID** : http://localhost:8080/api/Results/{id}
- **Swagger Documentation**: [http://localhost:5142/swagger/index.html](http://localhost:5142/swagger/index.html). Note that Swagger can be used only if `app.Environment.IsDevelopment()` is true.

## Getting Started

Follow these steps to get the project up and running on your local machine:

1. **Clone the repository**

   ```sh
   git clone <repository_url>
   ```

2. **Navigate to the project directory**

   ```sh
   cd nqueens_solver_api
   ```

3. **Build and run the services using Docker Compose**

   ```sh
   docker-compose up --build
   ```

   This command will build and start both the SQL Server and the web API containers.

4. **Access the API**

## Tests

### Unit Tests

Unit tests are available in the `/tests` directory. Run tests with:

```sh
dotnet test
```

## Deployment

Use Docker Compose to spin up all services, including MSSQL and the web API:

```sh
docker-compose up -d
```

## Contact

If you have any questions or feedback, feel free to reach out:

- **Email**: [peter.gospodinov@gmail.com](mailto:peter.gospodinov@gmail.com)
- **GitHub**: [https://github.com/peterGospodinov](https://github.com/peterGospodinov)
