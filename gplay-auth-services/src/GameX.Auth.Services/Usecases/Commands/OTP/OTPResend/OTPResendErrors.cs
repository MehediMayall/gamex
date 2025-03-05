namespace gamex.Auth.Services;

public class OTPResendErrors : ExceptionBase<OTPResendRequestDto>
{
    public static Error InvalidResendType() => new("Invalid Resend Type", $"Invalid Resend Type. Please try again with correct Resend Type.");
}