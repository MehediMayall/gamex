namespace gamex.Auth.Services;

public sealed class LoginCommandValidator : AbstractValidator<LoginCommand> {
    public LoginCommandValidator() {
        
        RuleFor(x => x.requestDto.Mobile).NotEmpty().MinimumLength(11).When(u => string.IsNullOrEmpty(u.requestDto.Email));
        RuleFor(x => x.requestDto.Email).NotEmpty().EmailAddress().When(u => string.IsNullOrEmpty(u.requestDto.Mobile));
        RuleFor(x => x.requestDto.Password).MinimumLength(6).WithMessage("Password is required. Length should be 6 digits long."); 
    }

}