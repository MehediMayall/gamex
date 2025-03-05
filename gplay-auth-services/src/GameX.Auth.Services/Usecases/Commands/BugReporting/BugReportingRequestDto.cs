namespace gamex.Auth.Services;


public sealed record BugReportingRequestDto(string Message, string Type, string Stack, string Level, DateTime Timestamp = default)
{
    public override string ToString() =>
        $"UI Bug: {nameof(Level)}: {Level}, {nameof(Message)}: {Message}, {nameof(Type)}: {Type}, {nameof(Stack)}: {Stack} , Timestamp:{Timestamp}";
};
