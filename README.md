# aspnet-core-3-signup-verification-api

ASP.NET Core 3.1 - API with Email Sign Up, Verification, Authentication & Forgot Password

## Commands

- Start the app: `F5`
- Add migration: `dotnet ef migrations add MigrationName`
- Update database: `dotnet ef database update`

`dotnet run` does not read `.vscode/launch.json`

## Running the API Locally

1. Download or clone the project code.

1. Configure the database connection string in the `.vscode/launch.json` file. For testing you can create a free postgresql database on heroku and copy the `DATABASE_URL` to the file.

1. Configure SMTP settings for email in the `.vscode/launch.json` file. For testing you can create a free account in one click at <https://ethereal.email/> and copy the options below the title SMTP configuration.

1. Start the api with `F5`, you should see the message Now listening on: <http://localhost:5000>, and you can view the Swagger API documentation at <http://localhost:5000/swagger>.

1. Test with Swagger or hook up with an example Angular or React application. If you use Swagger, you need to copy JWT to Authorization value.

### Before running in production

Before running in production also make sure that you update the `AppSettings__Secret` property in the `.vscode/launch.json` file, it is used to sign and verify JWT tokens for authentication, change it to a random string to ensure nobody else can generate a JWT with the same secret and gain unauthorized access to your api. A quick and easy way is join a couple of GUIDs together to make a long random string (e.g. from <https://www.guidgenerator.com/>).

## References

- [ASP.NET Core 3.1 - Boilerplate API with Email Sign Up, Verification, Authentication & Forgot Password](https://jasonwatmore.com/post/2020/07/06/aspnet-core-3-boilerplate-api-with-email-sign-up-verification-authentication-forgot-password#testing-postman)
- [Implementing Advanced RESTful Concerns with ASP.NET Core 3](https://github.com/KevinDockx/ImplementingAdvancedRESTfulConcernsAspNetCore3)
