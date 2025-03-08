namespace gamex.Auth.Services;

public interface ICodeGenerationService{
    Result<string> GenerateOTP();

    string GenerateCode(int MinimumLength=3);
}
