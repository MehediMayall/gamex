namespace gamex.Auth.Services;

public static class PasswordResetRequestEndpoint{
    public static void PasswordResetRequest(this IEndpointRouteBuilder app){                   
    
        app.MapPost("/password/reset/request", [AllowAnonymous] async(IMediator mediator, [FromBody] PasswordResetRequestDto  request, CancellationToken cancellationToken = default ) => {
            return Results.Ok(await mediator.Send(new PasswordResetRequestCommand(request), cancellationToken));
        })
        .Produces<Response<PasswordResetRequestResponseDto>>(StatusCodes.Status200OK)
        .WithTags("Password Reset")
        .WithSummary("First validate user request and then send otp to user mobile number or email")
        .WithOpenApi();


    }
}