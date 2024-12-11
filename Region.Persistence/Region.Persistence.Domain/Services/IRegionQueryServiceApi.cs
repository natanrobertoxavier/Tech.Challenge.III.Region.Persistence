using Region.Persistence.Domain.ResultServices;

namespace Region.Persistence.Domain.Services;
public interface IRegionQueryServiceApi
{
    Task<Result<ThereIsDDDNumberResult>> ThereIsDDDNumber(int dDD, string token);
}
