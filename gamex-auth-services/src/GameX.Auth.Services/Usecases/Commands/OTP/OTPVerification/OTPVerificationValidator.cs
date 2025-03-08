namespace gamex.Auth.Services;


internal  class OTPValidationValidator : AbstractValidator<OTPVerificationCommand>
{
    public OTPValidationValidator()
    {
        RuleFor(x => x.requestDto.UserId).NotEmpty();
        RuleFor(x => x.requestDto.OTP).NotEmpty().MinimumLength(6).MaximumLength(6);
    }
}   