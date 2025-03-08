namespace gamex.Auth.Services;

public class UserLogActivity : EntityBase<Guid>
{   
    public Guid? UserId { get; set; }
    public DateTime? LoginTime { get; set; }
    public string? IpAddress { get; set; }
    public string? UserAgent { get; set; }
    public string? DeviceInfo { get; set; }
    public string? Location { get; set; }
    public string? Status { get; set; }

    public string? Browser { get; set; }
    public string? BrowserVersion { get; set; }
    public string? BrowserEngine { get; set; }
    public string? OS { get; set; }
    public string? Device { get; set; }
    public string? DeviceType { get; set; }

    public bool IsLoginSuccessFull {get; set;}
    public string? AttemptedUserName { get; set; }
    public string? AttemptedPassword { get; set; }
    public string? LoginFailedReason { get; set; }

    public User User { get; set; }

    public static Result<UserLogActivity> New(LoginActivityDto loginActivityDto) {
        // IP//UserAgent//DeviceInfo//Location
        string decodedAgentDetails = "";
        if(loginActivityDto.AgentDetails is not null) 
            decodedAgentDetails = Encoding.UTF8.GetString(Convert.FromBase64String(loginActivityDto.AgentDetails)) ?? "";
        // if (string.IsNullOrEmpty(decodedAgentDetails))
            // return Result.Failure<UserLogActivity>(Error.New("", "Invalid Agent Details"));

            
        string[] activities = decodedAgentDetails.Split("//");

        // if (activities.Any() is false) 
        //     return Result.Failure<UserLogActivity>(Error.New("", "Invalid Agent Details"));

        string IpAddress, UserAgent, DeviceInfo, Location = "";
        IpAddress = (activities.Length > 0) ? activities[0] : "";
        UserAgent = (activities.Length > 1) ? activities[1] : "";
        DeviceInfo = (activities.Length > 2) ? activities[2] : "";
        Location = (activities.Length > 3) ? activities[3] : "";

        var agentDetail =  UserAgentParser.ParseUserAgent(UserAgent);

        return Result.Success<UserLogActivity>(
            new() {
            Id = Guid.NewGuid(),
            UserId = loginActivityDto.UserId,
            IpAddress = IpAddress,
            UserAgent = UserAgent,
            DeviceInfo = DeviceInfo,
            Location = Location,
            IsLoginSuccessFull = loginActivityDto.IsLoginSuccessFull,
            AttemptedUserName = loginActivityDto.AttemptedUserName,
            AttemptedPassword = loginActivityDto.AttemptedPassword,
            LoginFailedReason = loginActivityDto.LoginFailedReason,
            Browser = agentDetail.Browser,
            BrowserVersion = agentDetail.Version,
            BrowserEngine = agentDetail.Engine,
            OS = agentDetail.OperatingSystem,
            Device = agentDetail.Device,
            DeviceType = agentDetail.DeviceType
        });
    }

     
}