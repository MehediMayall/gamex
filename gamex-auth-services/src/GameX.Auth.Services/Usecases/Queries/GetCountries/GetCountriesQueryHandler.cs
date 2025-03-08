using System.Text.Json;

namespace gamex.Auth.Services;

public record GetCountriesQuery() : IRequest<Response<GetCountriesResponseDto>>{}
public sealed class GetCountriesQueryHandler : IRequestHandler<GetCountriesQuery, Response<GetCountriesResponseDto>>
{
 
    private readonly IUserRepository _userRepository;
    private readonly IHttpContextAccessor _httpContext;

    public GetCountriesQueryHandler(IUserRepository repo, IHttpContextAccessor httpContext)
    {
        _userRepository = repo;
        _httpContext = httpContext;
    }



    public async Task<Response<GetCountriesResponseDto>> Handle(GetCountriesQuery request, CancellationToken cancellationToken)
    {
         string filePath = "StaticContents/country-by-capital-city.json";   
        string jsonString = File.ReadAllText(filePath);

        var countries = JsonSerializer.Deserialize<List<Country>>(jsonString);

        return new GetCountriesResponseDto(countries);
        
    }



}