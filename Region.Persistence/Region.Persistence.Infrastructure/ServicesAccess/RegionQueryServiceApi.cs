using Region.Persistence.Domain.ResultServices;
using Region.Persistence.Domain.Services;
using Serilog;
using System.Net.Http.Headers;

namespace Region.Persistence.Infrastructure.ServicesAccess;
public class RegionQueryServiceApi(
    IHttpClientFactory httpClientFactory,
    ILogger logger) : Base, IRegionQueryServiceApi
{
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
    private readonly ILogger _logger = logger;

    public async Task<Result<ThereIsDDDNumberResult>> ThereIsDDDNumber(int dDD, string token)
    {
        _logger.Information($"{nameof(ThereIsDDDNumber)} - Initiating call to Region.Query Api. DDD: {dDD}.");

        var output = new Result<ThereIsDDDNumberResult>();

        try
        {
            var client = _httpClientFactory.CreateClient("RegionQueryApi");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var uri = string.Format("/api/v1/regionddd/there-is-ddd/{0}", dDD);

            var response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var responseApi = DeserializeResponseObject<Result<ThereIsDDDNumberResult>>(content);

                _logger.Information($"{nameof(ThereIsDDDNumber)} - Ended call to Region.Query Api. DDD: {dDD}.");

                return responseApi;
            }

            var failMessage = $"{nameof(ThereIsDDDNumber)} - An error occurred when calling the Region.Query Api. StatusCode: {response.StatusCode}";

            _logger.Error(failMessage);

            return output.Failure(failMessage);
        }
        catch (Exception ex)
        {
            var errorMessage = $"{nameof(ThereIsDDDNumber)} - An error occurred when calling the Region.Query Api. Error: {ex.Message}";

            _logger.Error(errorMessage);

            return output.Failure(errorMessage);
        }
    }
}
