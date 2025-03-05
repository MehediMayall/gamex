
namespace gamex.Auth.Services;


public sealed class UploadProfileImageErrors : ExceptionBase<UploadProfileImageRequestDto>
{
    public static Error GameNotFound(Guid gameId) =>
        new Error("Game Not Found", $"Couldn't find game using {gameId}. Please try again.");
    
}

