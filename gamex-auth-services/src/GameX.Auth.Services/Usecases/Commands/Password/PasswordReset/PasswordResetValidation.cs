namespace gamex.Auth.Services;


internal  class PasswordResetValidator : AbstractValidator<PasswordResetCommand>
{
    public PasswordResetValidator()
    {
        RuleFor(x => x.requestDto.UserId).NotEmpty();
        RuleFor(x => x.requestDto.Password).NotEmpty().MinimumLength(6).MaximumLength(16);
    }
}   