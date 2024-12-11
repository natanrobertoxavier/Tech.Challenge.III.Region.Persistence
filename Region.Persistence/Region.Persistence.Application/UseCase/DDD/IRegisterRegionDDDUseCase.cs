using Region.Persistence.Communication.Request;
using Region.Persistence.Communication.Response;

namespace Region.Persistence.Application.UseCase.DDD;
public interface IRegisterRegionDDDUseCase
{
    Task<Result<MessageResult>> RegisterDDDAsync(RequestRegionDDDJson request);
}
