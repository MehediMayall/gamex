namespace gamex.Auth.Services;


/// <summary>
/// Either email or mobile is required with password.
/// </summary>
/// <param name="Email"></param>
/// <param name="Mobile"></param>
/// <param name="Password"></param>
/// <param name="AgentDetails"></param>
/// <returns>Returns LoggedInResponseDto</returns>
public sealed record LoginRequestDto(
    string Email, 
    string Mobile, 
    string Password, 
    string AgentDetails
){
    public LoginActivityDto Get(bool IsLoginSuccessFull, string LoginFailedReason, Guid? UserId= null) {
        return new LoginActivityDto(){
            UserId = UserId,
            AttemptedUserName = $"{Mobile} {Email}",
            AttemptedPassword = IsLoginSuccessFull is true ? "" : Password, // No need store password if login is successful
            IsLoginSuccessFull = IsLoginSuccessFull,
            LoginFailedReason = LoginFailedReason,
            AgentDetails = AgentDetails
        };
    }
};