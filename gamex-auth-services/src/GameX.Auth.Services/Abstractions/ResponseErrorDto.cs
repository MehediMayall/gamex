namespace gamex.Auth.Services;
public class ResponseErrorDto 
{
    public string Status { get; private set; } 
    public object Data { get; private set; } = null;
    public List<Error> Errors {get;set;} = default!;
}