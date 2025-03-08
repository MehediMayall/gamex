namespace gamex.Auth.Services;

public record UserProfileQuery() : IRequest<Response<UserProfileResponseDto>>{}
public sealed class UserProfileQueryHandler : IRequestHandler<UserProfileQuery, Response<UserProfileResponseDto>>
{
 
    private readonly IPlayerRepository _repo;
    private readonly UserSessionDto _sessionUser;

    public UserProfileQueryHandler(IPlayerRepository repo, IUserSessionService sessionService)
    {
        _repo = repo;
        _sessionUser = sessionService.Get();
    }


    // Step 1: Get user using email/mobile and password
    // Step 2: Generate Token from that user
    // Step 3: Return UserProfile Response
    public async Task<Response<UserProfileResponseDto>> Handle(UserProfileQuery request, CancellationToken cancellationToken)
    {
        Guid userid = _sessionUser.UserId;

        var user = await _repo.GetUserProfile(userid);

        if (user == null)
            return LoginErrors.UserNotFoundById(userid);

        // Step 3: Return UserProfile Response
        return  user;
        
    }





}