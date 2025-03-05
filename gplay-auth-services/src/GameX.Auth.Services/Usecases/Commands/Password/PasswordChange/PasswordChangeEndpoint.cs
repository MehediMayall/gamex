namespace gamex.Auth.Services;

public static class PasswordChangeEndpoint{
    public static void PasswordChange(this IEndpointRouteBuilder app)
    {                   
    
        app.MapPost("/password/change", [Authorize] async(IMediator mediator, [FromBody] PasswordChangeRequestDto  request, CancellationToken cancellationToken = default ) => {
            return Results.Ok(await mediator.Send(new PasswordChangeCommand(request), cancellationToken));
        })
        .Produces<Response<PasswordChangeResponseDto>>(StatusCodes.Status200OK)
        .WithTags("Password")
        .WithSummary("Change user password")
        .WithOpenApi();

    }
}