namespace gamex.Auth.Services;

public static class SignupCompleteEndpoint{

    public static void SignupComplete(this IEndpointRouteBuilder app){                   
    
        app.MapPost("/signup/complete", [AllowAnonymous] async(IMediator mediator, [FromBody] SignupCompleteRequestDto user, CancellationToken cancellationToken = default ) => {
            return Results.Ok(await mediator.Send(new SignupCompleteCommand(user), cancellationToken));
        })
        .Produces<Response<LoggedInResponseDto>>(StatusCodes.Status200OK)
        .WithTags("Signup")
        .WithSummary("Complete Registration after otp verification is successful")
        .WithOpenApi();
    }
}