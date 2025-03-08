namespace gamex.Auth.Services;

public record PasswordResetCommand(PasswordResetDto requestDto) : IRequest<Response<PasswordResetResponseDto>>{}
public sealed class PasswordResetCommandHandler : IRequestHandler<PasswordResetCommand, Response<PasswordResetResponseDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork unitOfWork;

    public PasswordResetCommandHandler(IUserRepository repo, IUnitOfWork unitOfWork)
    {
        _userRepository = repo;
        this.unitOfWork = unitOfWork;
    }


    // Step 1: Get user using email/mobile and password
    // Step 2: Generate Token from that user
    // Step 3: Return Login Response
    public async Task<Response<PasswordResetResponseDto>> Handle(PasswordResetCommand request, CancellationToken cancellationToken)
    {
        Guid userid = request.requestDto.UserId;
 

        // Step 1: Load User Details from DB using UserID
        User? user = await _userRepository.Get(u=> u.Id == userid);

        if (user == null)
            return LoginErrors.UserNotFoundById(userid);

        string md5Password = Security.CreateMD5Hash(request.requestDto.Password);


        // Step 2: Update user with new password
        user.Password = md5Password;
        user.UpdatedOn = DateTime.UtcNow;

        await _userRepository.Update(user, u => u.Id == userid);
        await unitOfWork.SaveChangesAsync();


        // Step 3: Return Response
        return new PasswordResetResponseDto("Success");


        // Autologin
         // Step 2: Generate Token from that user
        // var tokenResult = await _tokenService.GetToken(userid);
        // if (tokenResult.IsFailure) return Response<LoggedInResponseDto>.Error(tokenResult.Error.Code, tokenResult.Error.Message);
        

        // // Step 3: Return Login Response
        // return Response<LoggedInResponseDto>.OK(LoggedInResponseDto.Get(user, tokenResult.Value));   
        
    }



}