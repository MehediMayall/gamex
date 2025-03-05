namespace gamex.Auth.Services;

public sealed class JWTTokenValidation : AbstractValidator<JWTSettings> {
    public JWTTokenValidation() {
        RuleFor(x => x.Key).NotEmpty().NotNull().WithMessage("Key is required.");
        RuleFor(x => x.Issuer).NotEmpty().NotNull().WithMessage("Issuer is required.");
        RuleFor(x => x.Audience).NotEmpty().NotNull().WithMessage("Audience is required.");        
    }
}