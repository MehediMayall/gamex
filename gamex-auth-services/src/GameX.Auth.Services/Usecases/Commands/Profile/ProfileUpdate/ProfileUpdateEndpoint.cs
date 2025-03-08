namespace gamex.Auth.Services;

public static class ProfileUpdateEndpoint{
    public static void ProfileUpdate(this IEndpointRouteBuilder app) {

 
        // Profile Update

        app.MapPut("/user/profile", [Authorize] async( [FromBody] ProfileUpdateRequestDto user,  IMediator mediator, 
                CancellationToken cancellationToken = default ) => 
            {

            return Results.Ok(await mediator.Send(new ProfileUpdateCommand(user), cancellationToken));
        })
        .Produces<Response<ProfileUpdateResponseDto>>(StatusCodes.Status200OK)
        .WithTags("Profile")
        .WithSummary("Update user account information")
        .WithOpenApi();

    }
}