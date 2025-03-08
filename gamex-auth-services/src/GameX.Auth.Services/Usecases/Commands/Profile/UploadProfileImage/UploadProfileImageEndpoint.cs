namespace gamex.Auth.Services;

public static class UploadProfileImageEndpoint{
    public static void UploadProfileImage(this IEndpointRouteBuilder app) {

 
        // Token
        app.MapPost("/profile/image/upload", 
                [Authorize] async(IMediator mediator, 
                [FromBody] UploadProfileImageRequestDto gameImages,  
                CancellationToken cancellationToken = default ) => 
            {
 
            return Results.Ok(await mediator.Send(new UploadProfileImageCommand(gameImages), cancellationToken));
        })
        .Produces<UploadProfileImageResponseDto>(StatusCodes.Status200OK)
        .WithTags("User Profile")
        .WithSummary("Save profile images")
        .WithOpenApi();

    }
}