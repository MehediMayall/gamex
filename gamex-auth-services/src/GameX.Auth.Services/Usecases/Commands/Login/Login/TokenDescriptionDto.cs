namespace gamex.Auth.Services;

public class TokenDescriptionDto {
    public DateTime? Expires  {get;init;} 
    public TimeSpan ExpiryDuration  {get;init;}
    public string Token  {get;init;}
}
