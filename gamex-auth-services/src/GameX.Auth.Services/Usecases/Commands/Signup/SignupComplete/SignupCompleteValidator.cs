namespace gamex.Auth.Services;

public sealed class SignupCompleteValidator : AbstractValidator<SignupCompleteCommand> {
    public SignupCompleteValidator() {
        RuleFor(x => x.OTPRequestDto.OTP).Length(6).WithMessage("OTP is required. Length should be 6 digits long."); 
        RuleFor(x => x.OTPRequestDto.UserId).NotEmpty().NotNull().WithMessage("UserId is required."); 
    }

}
