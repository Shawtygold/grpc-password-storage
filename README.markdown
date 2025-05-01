# Password Storage API with .NET 9, gRPC, PostgreSQL, Marten, Wolverine

This repository is a secure password management system. It consists of two main components: an **Authorization Server** for user authorization/authentication and a **Password Storage Server** for managing passwords (adding, deleting, editing, and retrieving). The project is designed to provide a robust and secure solution for storing and managing user credentials.

## Table of Contents

- [Api endpoints](#api-endpoints)
- [Tech Stack](#tech-stack)
- [Architecture](#architecture-overview)
- [Features](#features)

## API Endpoints
- **Authorization Server**:
  - `[AuthProtoService.RegisterUser]`: register a user
  - `[AuthProtoService.Login]`: login
  - `[AuthProtoService.LoginWithRefreshToken]`: login with refresh token
  - `[AuthProtoService.RevokeRefreshTokens]`: revoke all user refresh tokens
    
- **Password Server**:
  - `[PasswordProtoService.CreatePassword]`: add a new password
  - `[PasswordProtoService.UpdatePassword]`: update an existing password
  - `[PasswordProtoService.DeletePassword]`: delete a password
  - `[PasswordProtoService.GetPasswords]`: get all user passwords by UserID 

**Note**: All Password Server endpoints require a valid JWT token in the `Authorization` header.

## Features

- **Built with .NET 8**: Utilizes the latest features for efficient development.
- **gRPC**: Efficient communication protocol for service interactions.
- **Marten**: Document database for flexible data storage.
- **PostgreSQL**: Powerful relational database for data storage.
- **Wolverine**: Robust messaging solution between services including support for many popular transports


## Tech Stack

- **.NET 9**
- **gRPC**
- **PostgreSQL**
- **Marten**
- **Wolverine**
- **EF Core**

## Architecture Overview
- **Clean Architecture**
- **CQRS (Command Query Responsibility Segregation)**
- **Event Sourcing**

## Contributing

Contributions are welcome! Please follow these steps:

1. Fork the repository.
2. Create a new branch (`git checkout -b feature/your-feature`).
3. Commit your changes (`git commit -m 'Add your feature'`).
4. Push to the branch (`git push origin feature/your-feature`).
5. Create a pull request.

## License

Distributed under the MIT License. See `LICENSE` for more information.
