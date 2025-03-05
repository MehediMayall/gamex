namespace gamex.Auth.Services;

public sealed class UploadProfileImageCommandValidator : AbstractValidator<UploadProfileImageCommand> {
    public UploadProfileImageCommandValidator() {
        
        RuleFor(x => x.requestDto.ImageInBase64).NotEmpty().MinimumLength(50);

    }

 
}