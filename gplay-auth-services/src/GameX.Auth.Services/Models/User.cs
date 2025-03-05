namespace gamex.Auth.Services;

public sealed class User : EntityBase<Guid> {

    public Guid PlayerId { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string LastUsedPassword { get; set; }
    public Player Player{ get; set; }


    public static User Get(UserRegistrationRequestedDto requestDto, Guid UserId, Guid PlayerId) {

        if (requestDto == null) throw new ArgumentNullException(nameof(requestDto));
        if (UserId == Guid.Empty) UserId= Guid.NewGuid();
        string password =  Security.CreateMD5Hash(requestDto.Password);

        return new(){
            Id = UserId,
            PlayerId = PlayerId,
            UserName = requestDto.UserName,
            Password = password,
            LastUsedPassword = password
        };
    }
}
