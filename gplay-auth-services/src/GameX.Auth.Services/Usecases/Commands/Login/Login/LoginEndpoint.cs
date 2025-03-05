namespace gamex.Auth.Services;

public static class LoginEndpoint{
    public static void Login(this IEndpointRouteBuilder app) {

 
        // Token
        app.MapPost("/login", [AllowAnonymous] async(IUserAgentParserService userAgentService, IMediator mediator, [FromBody] LoginRequestDto user, CancellationToken cancellationToken = default ) => 
        {
            UserAgentInfo userAgentInfo = userAgentService.GetDeviceType();


            Log.Information($"New Login: {user.Email}, {user.Mobile} {userAgentInfo.DeviceType} {userAgentInfo.OperatingSystem} {userAgentInfo.Browser}");
            return Results.Ok(await mediator.Send(new LoginCommand(user), cancellationToken));
        })
        .Produces<Response<LoggedInResponseDto>>(StatusCodes.Status200OK)
        .WithTags("Login")
        .WithSummary("Verify user credentials and return token with user basic details")
        .WithOpenApi();

    }
}