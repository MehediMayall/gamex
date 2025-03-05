namespace gamex.Auth.Services;


internal  class OTPResendValidator : AbstractValidator<OTPResendCommand>
{
    public OTPResendValidator()
    {
        RuleFor(x => x.requestDto.UserId).NotEmpty();
        RuleFor(x => x.requestDto.ResendType).NotEmpty().MinimumLength(3).MaximumLength(12)
        .WithMessage(OTPResendErrors.InvalidResendType().Message);
    }
}   