namespace gamex.Auth.Services;

public record LogoutCommand() : IRequest<Response<LogoutResponseDto>>{}
public sealed class LogoutCommandHandler : IRequestHandler<LogoutCommand, Response<LogoutResponseDto>>
{
 
    private readonly IUserRepository _userRepository;
    private readonly IHttpContextAccessor _httpContext;

    public LogoutCommandHandler(IUserRepository repo, IHttpContextAccessor httpContext)
    {
        _userRepository = repo;
        _httpContext = httpContext;
    }


    // Step 1: Get user using email/mobile and password
    // Step 2: Generate Token from that user
    // Step 3: Return Logout Response
    public async Task<Response<LogoutResponseDto>> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        var user = _httpContext.HttpContext.User.Claims.FirstOrDefault();
        if ( user != null)
            Log.Information(user!.Value);
        _httpContext.HttpContext.User = null;

        // Step 3: Return Logout Response
        return "Successfull";
        
    }



}