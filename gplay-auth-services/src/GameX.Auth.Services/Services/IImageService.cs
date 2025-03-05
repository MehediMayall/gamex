namespace gamex.Auth.Services; 


public interface IImageService
{
    Task<Result<string>> SaveImage(string ImagePath, string ImageInBase64);
    Task<Result<string>> SaveImage(string ImagePath, string ImageInBase64, int Width, int Height);
    Result<bool> ResizeImage(string inputPath, string outputPath, int width, int height);
}
