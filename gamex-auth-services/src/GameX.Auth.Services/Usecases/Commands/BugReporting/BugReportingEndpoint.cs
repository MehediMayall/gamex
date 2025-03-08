namespace gamex.Auth.Services;

public static class BugReportingEndpoint{
    
    public static void BugReporting(this IEndpointRouteBuilder app){                   
    
        app.MapPost("/bug/reporting", [AllowAnonymous] async(IMediator mediator, [FromBody] IReadOnlyCollection<BugReportingRequestDto> bugs, CancellationToken cancellationToken = default ) => {
            return Results.Ok(await mediator.Send(new BugReportingCommand(bugs), cancellationToken));
        })
        .Produces<Response<BugReportingResponseDto>>(StatusCodes.Status200OK)
        .WithTags("Others")
        .WithSummary("Storing UI bugs into centralize log [Seq]")
        .WithOpenApi();

    }
}
