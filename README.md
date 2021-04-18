# web-scanner-v1

Web scanner is built using a Clean Architecture and CQRS architecture.

# Domain

Includes the solution's entities.

# Presentation

Includes controllers and the solution's front end Angular application.

# Application

Includes all the business logic for the solution. This mainly consists of Commands and Queries.

# Infrastructure

Includes all dependencies external to the solution, including the persistence and scanning functionality.

# Testing

Although the project contains no integration or unit tests due to time constraints, there are Newman (Postman) and k6 performance tests in the solution. These are configured to run within Azure DevOps pipelines (although these haven't been tested).

To run performance tests, from the root directory run:

```cd Presentation/K6```

```docker-compose build --no-cache --progress plain```

To run newman tests, from the root directory run:

```cd Presentation/Postman```

```docker-compose build --no-cache --progress plain```
