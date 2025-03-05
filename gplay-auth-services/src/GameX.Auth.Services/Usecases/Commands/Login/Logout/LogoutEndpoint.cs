namespace gamex.Auth.Services;

public static class LogoutEndpoint{
    public static void Logout(this IEndpointRouteBuilder app) {

 
        // Token
        app.MapPost("/logout",  [Authorize] async( IMediator mediator, CancellationToken cancellationToken = default ) => 
        {
            return Results.Ok(await mediator.Send(new LogoutCommand(), cancellationToken));
        })
        .Produces(StatusCodes.Status200OK)
        .WithTags("Login")
        .WithSummary("Remove user token from the session and log that user sign out")
        .WithOpenApi();

    }
}