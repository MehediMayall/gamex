namespace gamex.Auth.Services;

public static class SignupRequestEndpoint{


    public static void SignupRequest(this IEndpointRouteBuilder app){                   
    
        // Register
        app.MapPost("/signup/request", [AllowAnonymous] async(IMediator mediator, [FromBody] SignupRequestDto newUser, CancellationToken cancellationToken = default ) => {
            return Results.Ok(await mediator.Send(new SignupRequestCommand(newUser), cancellationToken));
        })
        .Produces<Response<LoggedInResponseDto>>(StatusCodes.Status200OK)
        .WithTags("Signup")
        .WithSummary("Signup Request for new user and otp sending")
        .WithOpenApi();

    }
}