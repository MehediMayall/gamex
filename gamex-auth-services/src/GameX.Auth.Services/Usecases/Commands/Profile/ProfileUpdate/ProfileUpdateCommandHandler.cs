namespace gamex.Auth.Services;

public record ProfileUpdateCommand(ProfileUpdateRequestDto requestDto) : IRequest<Response<ProfileUpdateResponseDto>>{}
public sealed class ProfileUpdateCommandHandler : IRequestHandler<ProfileUpdateCommand, Response<ProfileUpdateResponseDto>>
{
 
    private readonly IUserRepository _repo;
    private readonly IPlayerRepository _playerRepo;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContext;
    private readonly UserSessionDto _sessionUser;

    public ProfileUpdateCommandHandler(
        IUserRepository repo,  
        IPlayerRepository playerRepo,  
        IUnitOfWork unitOfWork,
        IHttpContextAccessor httpContext,
        IUserSessionService sessionService)
    {
        _repo = repo;
        _playerRepo = playerRepo;
        _unitOfWork = unitOfWork;
        _httpContext = httpContext;
        _sessionUser = sessionService.Get();
    }


    // Step 1: Get user using email/mobile and password
    // Step 2: Generate Token from that user
    // Step 3: Return UserProfile Response
    public async Task<Response<ProfileUpdateResponseDto>> Handle(ProfileUpdateCommand request, CancellationToken cancellationToken)
    {
        Guid userid = _sessionUser.UserId;

        User? existingUser = await _repo.Get(u => u.Id == userid && u.IsActive == true);
        if (existingUser == null) 
            return LoginErrors.UserNotFoundById(userid);

        // User
        var user = request.requestDto;
        existingUser.UserName = user.UserName;

        var existingamexer = await _playerRepo.Get(u => u.Id == existingUser.PlayerId && u.IsActive == true);

        // Player
        existingamexer.FirstName = user.FirstName;
        existingamexer.LastName = user.LastName;
        existingamexer.Email = string.IsNullOrEmpty(user.Email) ? null : user.Email.AsEmail();
        existingamexer.Mobile = string.IsNullOrEmpty(user.Mobile) ? null : user.Mobile.AsMobile();
        existingamexer.City = user.City;
        existingamexer.Country = user.Country;
        existingamexer.DateOfBirth = user.DateOfBirth;


        await _repo.Update(existingUser, u => u.Id == userid);
        await _playerRepo.Update(existingamexer, u => u.Id == existingUser.PlayerId);


        // Commit
        var commitResult =  await _unitOfWork.SaveChangesAsync();
        if (commitResult.IsFailure) 
            return commitResult.Error;

        // Step 3: Return UserProfile Response
        return  ProfileUpdateResponseDto.Get(existingamexer, existingUser);
        
    }




}