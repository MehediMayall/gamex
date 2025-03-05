using SkiaSharp;
namespace gamex.Auth.Services;

public sealed class ImageService : IImageService
{
    private readonly IFileOperationService fileOperationService;

    public ImageService(IFileOperationService fileOperationService)
    {
        this.fileOperationService = fileOperationService;
    }
    public async Task<Result<string>> SaveImage(string ImagePath, string ImageInBase64) {

        string parsedData;

        parsedData = ImageInBase64.Contains(',')
            ? ImageInBase64.Substring(ImageInBase64.IndexOf(',') + 1)
            : ImageInBase64;


        byte[] imageBytes = Convert.FromBase64String(parsedData);

        // string tempImage = ImagePath.Substring(0, ImagePath.Length - 4) + "_deleted.jpg";
        fileOperationService.CheckAndCreateDir(Directory.GetParent(ImagePath).Name);       
 

        if (File.Exists(ImagePath)) File.Delete(ImagePath);


        File.WriteAllBytes(ImagePath, imageBytes);

        // Image image = Image.FromFile(tempImage);
        // // image.RotateFlip(RotateFlipType.Rotate90FlipNone);
        // image.Save(ImagePath);
        // image.Dispose();

        // File.Delete(tempImage);


        return  "Success";
    }
    public async Task<Result<string>> SaveImage(string ImagePath, string ImageInBase64, int Width, int Height) {

        string parsedData;

        parsedData = ImageInBase64.Contains(',')
            ? ImageInBase64.Substring(ImageInBase64.IndexOf(',') + 1)
            : ImageInBase64;


        byte[] imageBytes = Convert.FromBase64String(parsedData);

        string tempImage = ImagePath.Substring(0, ImagePath.Length - 4) + "_deleted.jpg";
        fileOperationService.CheckAndCreateDir(Directory.GetParent(ImagePath).Name);       
        
        if (File.Exists(ImagePath)) File.Delete(ImagePath);


        File.WriteAllBytes(tempImage, imageBytes);

        var imageSaveResult = ResizeImage(tempImage, ImagePath, Width, Height);
        if (imageSaveResult.IsFailure) 
            return imageSaveResult.Error;

        File.Delete(tempImage);

        return  "Success";
    }

  
 

    public Result<bool> ResizeImage(string inputPath, string outputPath, int width, int height)
    {
        try
        {

            using (SKBitmap bitmap = SKBitmap.Decode(inputPath))
            using (SKBitmap resizedBitmap = bitmap.Resize(new SKImageInfo(width, height), SKFilterQuality.High))
            using (SKImage image = SKImage.FromBitmap(resizedBitmap))
            using (SKData data = image.Encode(SKEncodedImageFormat.Jpeg, 100))
            using (FileStream outputFile = File.OpenWrite(outputPath))
            {
                data.SaveTo(outputFile);
            }

            return true;
        }
        catch(Exception ex) { return Error.New(ex.GetAllExceptions()); }

    }
 

}