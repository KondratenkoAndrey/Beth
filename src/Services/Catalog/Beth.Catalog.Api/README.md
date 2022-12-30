Simple CRUD microservice with ASP.NET Core

### Database migrations
```sh
dotnet ef migrations add init_database -o .\Infrastructure\Migrations\
dotnet ef database update
```