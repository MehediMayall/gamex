namespace gamex.Auth.Services;

public record PasswordChangeCommand(PasswordChangeRequestDto requestDto) : IRequest<Response<PasswordChangeResponseDto>>{}
public sealed class PasswordChangeCommandHandler : IRequestHandler<PasswordChangeCommand, Response<PasswordChangeResponseDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserSessionDto _sessionUser;

    public PasswordChangeCommandHandler(IUserRepository repo, IUnitOfWork unitOfWork, IUserSessionService sessionService)
    {
        _userRepository = repo;
        _unitOfWork = unitOfWork;
        _sessionUser = sessionService.Get();
    }

 
    public async Task<Response<PasswordChangeResponseDto>> Handle(PasswordChangeCommand request, CancellationToken cancellationToken)
    {
        Guid userid = _sessionUser.UserId;

        // Step 1: Load User Details from DB using UserID
        User? user = await _userRepository.Get(u=> u.Id == userid && u.IsActive == true);

        if (user == null)
            return LoginErrors.UserNotFoundById(userid);

        string md5CurrentPassword = Security.CreateMD5Hash(request.requestDto.CurrentPassword);

        if (user.Password != md5CurrentPassword)
            return PasswordChangeErrors.InvalidCurrentPassword();

        string md5NewPassword = Security.CreateMD5Hash(request.requestDto.NewPassword);


        // Step 2: Update user with new password
        user.Password = md5NewPassword;
        user.LastUsedPassword = md5CurrentPassword;
        user.UpdatedOn = DateTime.UtcNow;

        await _userRepository.Update(user, u => u.Id == userid);
        await _unitOfWork.SaveChangesAsync();


        // Step 3: Return Response
        return new PasswordChangeResponseDto("Success");


        // Autologin
         // Step 2: Generate Token from that user
        // var tokenResult = await _tokenService.GetToken(userid);
        // if (tokenResult.IsFailure) return Response<LoggedInResponseDto>.Error(tokenResult.Error.Code, tokenResult.Error.Message);
        

        // // Step 3: Return Login Response
        // return Response<LoggedInResponseDto>.OK(LoggedInResponseDto.Get(user, tokenResult.Value));   
        
    }



}