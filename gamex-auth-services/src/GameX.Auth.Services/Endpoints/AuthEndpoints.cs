namespace gamex.Auth.Services;

public static class AuthEndpoints{
    public static void AddAuthEndpoints(this IEndpointRouteBuilder app){
        

        // Registration Request
        app.SignupRequest();


        // Complete REGISTRATION
        app.SignupComplete();

        // Resend OTP
        app.OTPResend();


        // User login
        app.Login();


        // Bug Reporting
        app.BugReporting();
        
        // Password Reset Request
        app.PasswordResetRequest();

        // OTP Verify
        app.OTPVerification();
        
        // Password Reset
        app.PasswordReset();

        // Logout
        app.Logout();

        // User Profile
        app.UserProfile();

        // Profile Update
        app.ProfileUpdate();

        // Upload Profile Image
        app.UploadProfileImage();

        // Password Change
        app.PasswordChange();


        // Get Countries
        app.GetCountries();



        // CodeGeneration
        app.MapGet("/generate/code/{length}", [AllowAnonymous] async([FromRoute] int length, ICodeGenerationService otpService,  CancellationToken cancellationToken = default ) => {
          
            return Results.Ok(otpService.GenerateCode(length));
        }).Produces(StatusCodes.Status200OK)
        .WithTags("Others")
        .WithOpenApi();


        // Database Migration
        app.MapGet("/database/migrate", [AllowAnonymous] async(UserDbContext context , CancellationToken cancellationToken = default ) => {
            await context.Database.MigrateAsync();
            return Results.Ok("Database migrated successfully.");
        }).Produces(StatusCodes.Status200OK)
        .WithTags("Others")
        .WithOpenApi();

    }
}