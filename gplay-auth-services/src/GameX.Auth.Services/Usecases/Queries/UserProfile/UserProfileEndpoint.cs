namespace gamex.Auth.Services;


public static class UserProfileEndpoint{
    /// <summary>
    /// Get logged user profile
    /// </summary>
    /// <returns>Firstname, lastname, email, mobile and email</returns>
    public static void UserProfile(this IEndpointRouteBuilder app) {
     
        app.MapGet("/user/profile", [Authorize] async(  IMediator mediator, CancellationToken cancellationToken = default ) => {

            return Results.Ok(await mediator.Send(new UserProfileQuery(), cancellationToken));
            
        })
        .Produces<Response<UserProfileResponseDto>>(StatusCodes.Status200OK)
        .WithTags("Profile")
        .WithSummary("Get an user account information")
        .WithOpenApi();

    }

}