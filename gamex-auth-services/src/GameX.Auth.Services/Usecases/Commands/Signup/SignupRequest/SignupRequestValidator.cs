namespace gamex.Auth.Services;


internal  class SignupRequestCommandValidator : AbstractValidator<SignupRequestCommand>
{
    public SignupRequestCommandValidator()
    {
        RuleFor(x => x.requestDto.Mobile).NotEmpty().MinimumLength(11).When(u => string.IsNullOrEmpty(u.requestDto.Email));

        RuleFor(x => x.requestDto.Email).NotEmpty().EmailAddress().When(u => string.IsNullOrEmpty(u.requestDto.Mobile));

        RuleFor(x => x.requestDto.Password).NotEmpty().MinimumLength(6).MaximumLength(16);
        // RuleFor(x => x.requestDto.FirstName).NotEmpty().MinimumLength(3);
        // RuleFor(x => x.requestDto.LastName).NotEmpty().MinimumLength(3);
        // RuleFor(x => x.requestDto.DateOfBirth).Must(HaveMinimumAge).WithMessage("You must be at least 13 years old.");
    }

    private bool HaveMinimumAge(DateOnly dateOfBirth)
    {
        var minimumAge = 13;
        var today = DateTime.Today;
        var age = today.Year - dateOfBirth.Year;
        if (today.Month < dateOfBirth.Month || (today.Month == dateOfBirth.Month && today.Day < dateOfBirth.Day))
        {
            age--;
        }
        return age >= minimumAge;
    }

}   