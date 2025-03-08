namespace gamex.Auth.Services;

public static class PasswordResetEndpoint{
    public static void PasswordReset(this IEndpointRouteBuilder app){                   
    
        app.MapPost("/password/reset", [AllowAnonymous] async(IMediator mediator, [FromBody] PasswordResetDto  request, CancellationToken cancellationToken = default ) => {
            return Results.Ok(await mediator.Send(new PasswordResetCommand(request), cancellationToken));
        })
        .Produces<Response<PasswordResetResponseDto>>(StatusCodes.Status200OK)
        .WithTags("Password Reset")
        .WithSummary("Reset user password after otp verification is successful")
        .WithOpenApi();

    }
}