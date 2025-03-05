namespace gamex.Auth.Services;

public sealed class CodeGenerationService : ICodeGenerationService{

    private readonly string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

    public string GetGameCode() =>
        string.Join("", Enumerable.Repeat(chars, 6).Select(s => s[new Random().Next(s.Length)]));

    public Result<string> GenerateOTP(){
        string otp = new Random().Next(100000, 999999).ToString();


        if (otp.Length != 6) 
            return SignupRequestErrors.OTPGenerationFailed();

        return otp;
    }
    public string GenerateCode(int MinimumLength=3) =>
        string.Join("", Enumerable.Repeat(chars, MinimumLength).Select(s => s[new Random().Next(s.Length)]));
}