namespace gamex.Auth.Services;

public sealed record OTPResendRequestDto(Guid UserId, string ResendType)
{
    public UserRegistrationRequestedDto GetUserRegistrationRequestedDto(UserRegistrationRequestedDto user, string OTP) => 
    new(){

        FirstName = user.FirstName, 
        LastName = user.LastName,  
        Email = user.Email, 
        Mobile = user.Mobile, 
        DateOfBirth = user.DateOfBirth,  
        Password = user.Password,
        UserName =  user.UserName,
        OTP = OTP
    };
};