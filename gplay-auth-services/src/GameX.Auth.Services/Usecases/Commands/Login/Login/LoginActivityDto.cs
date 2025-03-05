namespace gamex.Auth.Services;

public sealed record LoginActivityDto{
    public Guid? UserId {get;init;}
    public string AgentDetails {get;init;} 
    public bool IsLoginSuccessFull {get;init;} 
    public string AttemptedUserName {get;init;} 
    public string AttemptedPassword {get;init;}
    public string LoginFailedReason {get;init;}
}