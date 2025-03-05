namespace gamex.Auth.Services;


public static class GetCountriesEndpoint{
    /// <summary>
    /// Get logged user profile
    /// </summary>
    /// <returns>Firstname, lastname, email, mobile and email</returns>
    public static void GetCountries(this IEndpointRouteBuilder app) {

        app.MapGet("/country/list", [AllowAnonymous] async(  IMediator mediator, 
                CancellationToken cancellationToken = default ) => 
            {

            return Results.Ok(await mediator.Send(new GetCountriesQuery(), cancellationToken));
        })
        .Produces<GetCountriesResponseDto>(StatusCodes.Status200OK)
        .WithTags("Profile")
        .WithSummary("To set user location. City, Country = Location")
        .WithOpenApi();

    }

}