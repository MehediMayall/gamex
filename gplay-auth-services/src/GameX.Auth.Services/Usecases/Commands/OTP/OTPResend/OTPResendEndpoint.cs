namespace gamex.Auth.Services;

public static class OTPResendEndpoint{
    public static void OTPResend(this IEndpointRouteBuilder app){                   
    
        app.MapPost("/otp/resend", [AllowAnonymous] async(IMediator mediator, [FromBody] OTPResendRequestDto  request, CancellationToken cancellationToken = default ) => {
            return Results.Ok(await mediator.Send(new OTPResendCommand(request), cancellationToken));
        })
        .Produces<Response<OTPResendResponseDto>>(StatusCodes.Status200OK)
        .WithTags("OTP")
        .WithSummary("Generate OTP and store it in cache. Send otp notification again to user mobile number or email")
        .WithOpenApi();


    }
}