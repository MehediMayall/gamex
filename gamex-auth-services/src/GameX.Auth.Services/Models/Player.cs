namespace gamex.Auth.Services;

public sealed class Player : EntityBase<Guid> {
    public string? FirstName { get; set; }
    public string LastName { get; set; }
    public Email? Email { get; set; }
    public Mobile? Mobile { get; set; }
    public DateOnly? DateOfBirth { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }
    public string? AvatarName { get; set; }
    public string? ProfileImagename { get; set; }
    public RegistrationSourceIds RegistrationSourceId { get; set; }
    public User User { get; set; } = new User();

    public static Player Get(
        UserRegistrationRequestedDto requestDto, 
        RegistrationSourceIds sourceId, 
        Guid PlayerId = default
    ) {

        if (requestDto == null) throw new ArgumentNullException(nameof(requestDto));
        if (PlayerId == Guid.Empty) PlayerId = Guid.NewGuid();
        

        return new(){
            Id = PlayerId,
            FirstName = requestDto.FirstName,
            LastName = requestDto.LastName,
            Email = string.IsNullOrEmpty(requestDto.Email) ? null : requestDto.Email.AsEmail(), 
            Mobile = string.IsNullOrEmpty(requestDto.Mobile)  ? null : requestDto.Mobile.AsMobile(), 
            DateOfBirth = requestDto.DateOfBirth,
            RegistrationSourceId = sourceId,
            City = requestDto.City,
            Country = requestDto.Country,
            AvatarName = requestDto.AvatarName,
        };
    }

    public static Result<Player> GetGPPlayer(
        string MSISDN, 
        string ClientName, 
        IUserNameGenerator userNameGenerator, 
        ICodeGenerationService codeGenerationService
    ) 
    {
        
        Guid PlayerId = Guid.NewGuid();
        string UserName = userNameGenerator.GetName();
        string md5Password = Security.CreateMD5Hash(codeGenerationService.GenerateCode(8));

        return new Player { 
            Id = PlayerId, 
            Mobile = MSISDN.AsMobile(),
            DateOfBirth = DateOnly.Parse("2000-01-01"),
            City = "Dhaka",
            Country = "Bangladesh",
            FirstName = ClientName,
            LastName = "Player",
            RegistrationSourceId = ClientName == "GP" ? RegistrationSourceIds.GP : RegistrationSourceIds.Skitto,

            User = new User() {
                Id = Guid.NewGuid(),
                PlayerId = PlayerId,
                UserName = UserName,
                Password = md5Password,
            }
        };
    }
}