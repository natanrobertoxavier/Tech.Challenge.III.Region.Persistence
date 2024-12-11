using Region.Persistence.Domain.ResultServices;

namespace Region.Persistence.Domain.Services;
public interface IUserQueryServiceApi
{
    Task<Result<UserResult>> RecoverByEmailAsync(string email);
}
