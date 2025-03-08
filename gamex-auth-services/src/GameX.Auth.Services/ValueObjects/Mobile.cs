namespace gamex.Auth.Services;

public sealed record Mobile {
    public string Value {get;}
    private Mobile(string value){Value = value;}
    public static Mobile New(string value){
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullException(nameof(value), "Mobile can't be empty");
        if (!IsValidMobile(value)) 
            throw new ArgumentException("Mobile is not valid", nameof(value));   
        return new Mobile(value);     
    }

    private static bool IsValidMobile(string value) =>
        value.Length >=10 && value.Length <= 20 ? true : false;

}

public static class MobileExtensions {
    public static Mobile AsMobile(this string value) => Mobile.New(value);
}
