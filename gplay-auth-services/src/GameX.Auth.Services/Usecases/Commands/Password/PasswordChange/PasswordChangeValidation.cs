namespace gamex.Auth.Services;


internal  class PasswordChangeValidator : AbstractValidator<PasswordChangeCommand>
{
    public PasswordChangeValidator()
    {
        RuleFor(x => x.requestDto.CurrentPassword).NotEmpty().MinimumLength(6).MaximumLength(16);
        RuleFor(x => x.requestDto.NewPassword).NotEmpty().MinimumLength(6).MaximumLength(16);
    }
}   