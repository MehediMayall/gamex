namespace gamex.Auth.Services;


/// <summary>
/// Either email or mobile is required with password.
/// </summary>
/// <param name="FirstName"></param>
/// <param name="LastName"></param>
/// <param name="DateOfBirth"></param>
/// <param name="Email"></param>
/// <param name="Mobile"></param>
/// <param name="Password"></param>
/// <returns>Returns LoggedInResponseDto</returns>
public sealed record SignupRequestDto(
    string FirstName, 
    string LastName, 
    DateOnly DateOfBirth,  
    string Email, 
    string Mobile,  
    string Password,
    string City,
    string Country
)
{

    public UserRegistrationRequestedDto GetUserRegistrationRequestedDto(string OTP) {
        string firstName = string.IsNullOrEmpty(FirstName) ? "Anonymous" : FirstName.Trim();
        string lastName = string.IsNullOrEmpty(LastName) ? "User" : LastName.Trim();
        return new(){

            FirstName = firstName, 
            LastName =  lastName,  
            Email = Email, 
            Mobile = Mobile, 
            DateOfBirth = DateOfBirth,  
            Password = Password,
            City = City,
            Country = Country,
            OTP = OTP        
        };
    }
};