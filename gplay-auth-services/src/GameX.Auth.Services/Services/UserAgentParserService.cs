using System.Text.RegularExpressions;

namespace gamex.Auth.Services;
 
public interface IUserAgentParserService
{
    UserAgentInfo GetDeviceType();
}

public sealed class UserAgentParserService(IHttpContextAccessor _httpContext) : IUserAgentParserService
{

  
    public UserAgentInfo ParseUserAgent(string userAgent)
    {
        var info = new UserAgentInfo();

        // Extract Operating System
        if (userAgent.Contains("Windows NT 10.0"))
        {
            info.OperatingSystem = "Windows 10";
        }
        else if (userAgent.Contains("Macintosh"))
        {
            info.OperatingSystem = "macOS";
        }
        else if (userAgent.Contains("Linux"))
        {
            info.OperatingSystem = "Linux";
        }
        else if (userAgent.Contains("Android"))
        {
            info.OperatingSystem = "Android";
        }
        else if (userAgent.Contains("iPhone"))
        {
            info.OperatingSystem = "iOS";
        }

        // Extract Browser
        if (userAgent.Contains("Chrome/"))
        {
            info.Browser = "Chrome";
            info.Version = ExtractVersion(userAgent, "Chrome/");
        }
        else if (userAgent.Contains("Safari/"))
        {
            info.Browser = "Safari";
            info.Version = ExtractVersion(userAgent, "Safari/");
        }
        else if (userAgent.Contains("CriOS/"))
        {
            info.Browser = "Chrome (iOS)";
            info.Version = ExtractVersion(userAgent, "CriOS/");
        }

        // Extract Device Type
        if (userAgent.Contains("Mobile"))
        {
            info.DeviceType = "Mobile";
        }
        else if (userAgent.Contains("Macintosh") || userAgent.Contains("Windows NT"))
        {
            info.DeviceType = "Desktop";
        }

        // Detect Rendering Engine
        var engineMatch = Regex.Match(userAgent, @"AppleWebKit/([\d.]+)");
        if (engineMatch.Success)
        {
            info.Engine = $"AppleWebKit/{engineMatch.Groups[1].Value}";
        }

        // Detect OS & Device
        if (userAgent.Contains("Macintosh"))
            info.Device = "Mac";
        else if (userAgent.Contains("Windows NT 10.0"))
            info.Device = "PC" ;
        else if (userAgent.Contains("Windows NT 6.1"))
            info.Device = "PC";

        else if (userAgent.Contains("Android"))
        {
            var androidVersionMatch = Regex.Match(userAgent, @"Android (\d+)");
            string androidVersion = androidVersionMatch.Success ? androidVersionMatch.Groups[1].Value : "Unknown";
            info.Device = "Mobile";
        }
        else if (userAgent.Contains("iPhone"))
        {
            var iosVersionMatch = Regex.Match(userAgent, @"iPhone OS (\d+_\d+)");
            string iosVersion = iosVersionMatch.Success ? iosVersionMatch.Groups[1].Value.Replace('_', '.')
                                                        : "Unknown";
            info.Device = "iPhone";
        }

        return info;
    }


    public UserAgentInfo GetDeviceType() 
    {
        try
        {
            return ParseUserAgent(_httpContext.HttpContext.Request.Headers["User-Agent"].ToString());            
        }
        catch(Exception ex) {
            Log.Error($"gamex Auth => GetDeviceType : {ex.GetAllExceptions()}");
            return null;
        }
    }

    private string ExtractVersion(string userAgent, string key)
    {
        var regex = new Regex($"{key}([\\d.]+)");
        var match = regex.Match(userAgent);
        return match.Success ? match.Groups[1].Value : "Unknown";
    }
}