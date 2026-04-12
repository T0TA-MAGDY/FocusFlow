# Focus Flow Backend (.NET 8 + Clean Architecture)

Production-ready backend for Focus Flow using .NET 8, Entity Framework Core, SQL Server, MediatR (CQRS), JWT authentication, and ASP.NET Core rate limiting.

## Project Structure

- `FocusFlow.Domain` - Entities, enums, repository interfaces
- `FocusFlow.Application` - DTOs, MediatR commands/queries/handlers, app-level interfaces
- `FocusFlow.Infrastructure` - EF Core DbContext, repository implementations, JWT services, persistence wiring
- `FocusFlow.API` - Controllers, middleware, startup/configuration

## Prerequisites

- .NET 8 SDK
- SQL Server (LocalDB, SQL Express, or full SQL Server via SSMS)
- EF Core CLI tools: `dotnet tool install --global dotnet-ef`

## Configuration

Update `FocusFlow.API/appsettings.json`:
- `ConnectionStrings:DefaultConnection`
- `Jwt:SecretKey` (use a long random value)
- `Jwt:Issuer`, `Jwt:Audience`, `Jwt:ExpirationMinutes`

Example SQL Server local connection string:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=FocusFlowDB;Trusted_Connection=True;TrustServerCertificate=True"
}
```

## Database & Migrations

From solution root:

```bash
dotnet ef migrations add InitialCreate --project FocusFlow.Infrastructure --startup-project FocusFlow.API --output-dir Persistence/Migrations
dotnet ef database update --project FocusFlow.Infrastructure --startup-project FocusFlow.API
```

The API also runs `Database.Migrate()` on startup.

## Run the API

```bash
dotnet run --project FocusFlow.API
dotnet run --project FocusFlow.API --launch-profile http
dotnet run --project FocusFlow.API --launch-profile https
```

- HTTP: `http://localhost:5000`
- HTTPS: `https://localhost:5001`

## Publish

```powershell
./publish.ps1
```

## Hosting on IIS / MonsterASP (and similar)

**Configure production in one place:** open `FocusFlow.API/appsettings.Production.json`. It contains:

- A **SQL Server connection string template** (`Server=...;User Id=...;Password=...`) — replace `YOUR_*` placeholders with values from your hosting SQL panel. Do not use `Server=.;Trusted_Connection=True` on shared hosting.
- **JWT** settings — replace `Jwt:SecretKey` with a long random value (32+ characters).
- A **`Deployment`** section — short checklist strings (environment variable, migrations, HTTPS/Swagger, optional Azure SQL example).

**Load this file on the server:** set `ASPNETCORE_ENVIRONMENT=Production` in the hosting control panel (or `web.config` `environmentVariables`) so `appsettings.Production.json` merges over `appsettings.json`.

**HTTP 500.30 – ASP.NET Core app failed to start** means the process crashed during startup. Common causes: wrong SQL connection string, database unreachable, or migration failure. This project does **not** hardcode Kestrel ports for IIS (ANCM owns binding).

**See the real error on the server**

- `FocusFlow.API/web.config` publishes with `stdoutLogEnabled="true"` and `stdoutLogFile=".\logs\stdout"`.
- Create a `logs` folder next to the site if needed, reproduce the error, then read the newest `stdout_*.log` file.

**HTTPS behind a reverse proxy**

- `Program.cs` configures `ForwardedHeaders` (`X-Forwarded-For`, `X-Forwarded-Proto`) when the host terminates TLS.

## Error Format

All API errors return:

```json
{ "error": "message" }
```

## API Endpoints

| Method | URL | JWT Required | Request Example | Response Example |
|---|---|---|---|---|
| POST | `/api/auth/register` | No | `{ "username": "ahmed", "email": "ahmed@example.com", "password": "P@ssw0rd" }` | `{ "userId": "...", "username": "ahmed", "email": "ahmed@example.com", "token": "..." }` |
| POST | `/api/auth/login` | No | `{ "email": "ahmed@example.com", "password": "P@ssw0rd" }` | `{ "userId": "...", "username": "ahmed", "email": "ahmed@example.com", "token": "..." }` |
| GET | `/api/tasks` | Yes | _No body_ | `[ { "id": "...", "title": "Math revision", "priority": "High", "isCompleted": false, "createdAt": "..." } ]` |
| POST | `/api/tasks` | Yes | `{ "title": "Math revision", "priority": "High" }` | `{ "id": "...", "title": "Math revision", "priority": "High", "isCompleted": false, "createdAt": "..." }` |
| PATCH | `/api/tasks/{id}` | Yes | `{ "isCompleted": true }` | `{ "id": "...", "title": "Math revision", "priority": "High", "isCompleted": true, "createdAt": "..." }` |
| DELETE | `/api/tasks/{id}` | Yes | _No body_ | `204 No Content` |
| POST | `/api/sessions` | Yes | `{ "taskId": "...", "startTime": "2026-04-10T13:00:00Z", "durationSeconds": 1500 }` | `{ "id": "...", "taskId": "...", "startTime": "...", "durationSeconds": 1500, "status": "Completed" }` |

## Validation Rules

- Task `title`: required, length 1..100
- Task `priority`: exactly `Low`, `Medium`, or `High`
- Session `durationSeconds`: 1..86400
- Session `taskId`: must exist and belong to authenticated user

## Rate Limiting

- 10 requests per second per IP
- 429 response: `{ "error": "Too many requests." }`

## Database Schema (Code-First)

- `Users` (`Id`, `Username`, `Email`, `PasswordHash`, `CreatedAt`)
- `Tasks` (`Id`, `UserId`, `Title`, `Priority`, `IsCompleted`, `CreatedAt`)
- `FocusSessions` (`Id`, `UserId`, `TaskId`, `StartTime`, `DurationSeconds`, `Status`)

## Notes

- No CORS configuration is included by design.
- Logging uses console output via `ILogger`.
- Backend stores completed sessions only; timer logic remains in frontend.
- Swagger UI is available at `/swagger`.
