using System.Net.Mail;

namespace gamex.Auth.Services;

public sealed record Email{
    public string Value {get;}
    private Email(string value){Value = value;}
    public static Email New(string value){
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullException(nameof(value), "Email can't be empty");
        if (!IsValidEmail(value)) 
            throw new ArgumentException("Email is not valid", nameof(value));   
        return new Email(value.ToLower());
    }

    private static bool IsValidEmail(string value) {
        try
        {
            return new MailAddress(value).Address == value;
        }
        catch{ return false;}
    }
};

public static class EmailExtensions {
    public static Email AsEmail(this string value) => Email.New(value);
}
