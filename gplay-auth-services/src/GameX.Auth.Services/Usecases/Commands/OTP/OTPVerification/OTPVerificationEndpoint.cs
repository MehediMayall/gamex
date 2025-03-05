namespace gamex.Auth.Services;

public static class OTPVerificationEndpoint{
    public static void OTPVerification(this IEndpointRouteBuilder app){                   
    
        app.MapPost("/otp/verify", [AllowAnonymous] async(IMediator mediator, [FromBody] OTPVerificationRequestDto  request, CancellationToken cancellationToken = default ) => {
            return Results.Ok(await mediator.Send(new OTPVerificationCommand(request), cancellationToken));
        })
        .Produces<Response<OTPVerificationResponseDto>>(StatusCodes.Status200OK)
        .WithTags("OTP")
        .WithSummary("Validate OTP and compare with OTP in cache")
        .WithOpenApi();


    }
}