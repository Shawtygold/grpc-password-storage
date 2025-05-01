# Password Storage API with .NET 9, gRPC, PostgreSQL, Marten, Wolverine

This repository is a secure password management system. It consists of two main components: an **Authorization Server** for user authorization/authentication and a **Password Storage Server** for managing passwords (adding, deleting, editing, and retrieving). The project is designed to provide a robust and secure solution for storing and managing user credentials.

<!--## Features

- **Authorization Server**:
  - Registration
  - Login
  - Login with refresh token
  - Revoke refresh tokens

- **Password Storage Server**:
  - Add new passwords
  - Retrieve stored passwords securely
  - Edit existing password entries
  - Delete passwords
  - Encrypted storage to ensure data security
 
- **Client**:
  - Soon-->

## Table of Contents

- [Nigger](#Tech-Stack)
- [Текст ссылки](#sss)
- [отображаемое название](#architecture-overview)

## API Endpoints
- **Authorization Server**:
  - `[AuthProtoService.RegisterUser]`: register
  - `[AuthProtoService.Login]`: login
  - `[AuthProtoService.LoginWithRefreshToken]`: login with refresh token
  - `[AuthProtoService.RevokeRefreshTokens]`: revoke all user refresh tokens
    
- **Password Server**:
  - `[PasswordProtoService.CreatePassword]`: add a new password
  - `[PasswordProtoService.UpdatePassword]`: update an existing password
  - `[PasswordProtoService.DeletePassword]`: delete a password
  - `[PasswordProtoService.GetPasswords]`: get all user passwords by UserID 

**Note**: All Password Server endpoints require a valid JWT token in the `Authorization` header.

## Tech Stack

- **.NET 9**
- **gRPC**
- **PostgreSQL**
- **Marten**
- **Wolverine**
- **EF Core**

## Architecture Overview
- **Clean Architecture**:
  - A design approach organizing code into layers (Domain, Application, Infrastructure, Presentation (Web API)) to keep business logic independent of frameworks and external systems. Goal: modularity, testability, maintainability.
- **CQRS (Command Query Responsibility Segregation)**:
  - A pattern separating read (Queries) and write (Commands) operations into distinct models. Reads are optimized for speed, writes for consistency.
- **Event Sourcing**:
  - A pattern where state is derived by storing and replaying a sequence of events representing changes, rather than storing the current state. Ensures auditability and enables rebuilding state from events
<a name="sss"></a> 
## Security Considerations

- Use strong, unique keys for JWT and encryption.
- Regularly rotate secrets and keys.
- Ensure your database is secured and not publicly accessible.
- Enforce HTTPS in production to protect data in transit.

## Contributing

Contributions are welcome! Please follow these steps:

1. Fork the repository.
2. Create a new branch (`git checkout -b feature/your-feature`).
3. Commit your changes (`git commit -m 'Add your feature'`).
4. Push to the branch (`git push origin feature/your-feature`).
5. Open a pull request.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

## Contact

For questions or support, please open an issue in the repository or contact the maintainer at [your-email@example.com].
