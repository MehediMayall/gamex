using Microsoft.Extensions.FileProviders;

namespace gamex.Auth.Services;
public static class RegisterStaticResourceExtension
{
    public static void RegisterStaticResource(this WebApplicationBuilder builder, WebApplication app, IConfiguration configuration)
    {
        
        // ******************************* STATIC FILES AND ATTACHMENT FOLDER
        var directoryNames = configuration.GetSection("AttachmentDirectories").Get<AttachmentDirectories>();
        string withAttchmentFolder = Path.Combine(Directory.GetCurrentDirectory(), "attachments");
        string staticContentFolder = Path.Combine(Directory.GetCurrentDirectory(), "StaticContents");
        CheckAndCreateDir(withAttchmentFolder);

        // Attachments Folder
        if (Directory.Exists(withAttchmentFolder))
            RegisterPath(app, "attachments", withAttchmentFolder);
        else 
            Log.Error($"GAME FILES FOLDER NOT FOUND IN >>{withAttchmentFolder}");


        // GAME IMAGES        
        string imagesDirectory = Path.Combine(withAttchmentFolder, directoryNames.IMAGES);
        CheckAndCreateDir(imagesDirectory);
        
        if (Directory.Exists(imagesDirectory))
            RegisterPath(app, "images", imagesDirectory);
        else 
            Log.Error($"GAME IMAGES FOLDER NOT FOUND IN >>{imagesDirectory}");


        Log.Information("RegisterStaticResource: SUCCECSSFULL");



        // Static Contents
        string staticContentDirectory = staticContentFolder;  
        CheckAndCreateDir(staticContentDirectory);
        
        if (Directory.Exists(staticContentDirectory))
            RegisterPath(app, "staticcontents", staticContentDirectory);
        else 
            Log.Error($"GAME GENRE FOLDER NOT FOUND IN >>{staticContentDirectory}");


        Log.Information("RegisterStaticResource: SUCCECSSFULL");

        
    }

    private static void RegisterPath(WebApplication app, string RequestPath, string PhysicalPath)
    {
        try
        {
            app.UseFileServer(new FileServerOptions
            {
                FileProvider = new PhysicalFileProvider(PhysicalPath),
                RequestPath = new PathString($"/{RequestPath}")
            });

            Log.Information($"{RequestPath.ToUpper()}: SUCCECSSFULL");
        }
        catch (Exception ex) { Log.Fatal(ex, ""); }
    }

    public static Boolean CheckAndCreateDir(string Dir)
    {
        try
        {
            Directory.CreateDirectory(Dir);
            if (Directory.Exists(Dir)) return true;
        }
        catch { }


        string[] dr = Dir.Split('\\');
        string curDir = "";

        foreach (string st in dr)
        {

            if (string.IsNullOrEmpty(st) || st == "\\" || st == @"\") continue;

            if (curDir.Length == 0) curDir = st;
            else curDir = curDir + "\\" + st;
            DirectoryInfo di = new DirectoryInfo(curDir);
            if (di.Exists == false)
            {
                di.Create();
            }
        }

        return true;
    }
}
