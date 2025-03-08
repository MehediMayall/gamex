namespace gamex.Auth.Services;

public sealed record BugReportingCommand(IReadOnlyCollection<BugReportingRequestDto> RequestDto) : IRequest<Response<BugReportingResponseDto>>;

public sealed class BugReportingCommandHandler : IRequestHandler<BugReportingCommand, Response<BugReportingResponseDto>>
{
     
    public async Task<Response<BugReportingResponseDto>> Handle(BugReportingCommand request, CancellationToken cancellationToken)
    {
        foreach (var bug in request.RequestDto)
            Log.Information( bug.ToString());

        return new BugReportingResponseDto("Success");
    }
}
