namespace gamex.Auth.Services;

public sealed record GetCountriesResponseDto(List<Country> Countries);
public sealed record Country(string country, string city);