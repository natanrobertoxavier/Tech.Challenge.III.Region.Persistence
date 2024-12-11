using Microsoft.AspNetCore.Mvc;
using Region.Persistence.Api.Filters;
using Region.Persistence.Application.UseCase.DDD;
using Region.Persistence.Communication.Request;
using Region.Persistence.Communication.Response;

namespace Region.Persistence.Api.Controllers.v1;

[ServiceFilter(typeof(AuthenticatedUserAttribute))]
public class RegionDDDController : TechChallengeController
{
    [HttpPost]
    [ProducesResponseType(typeof(Result<MessageResult>), StatusCodes.Status200OK)]
    public async Task<IActionResult> RegisterDDD(
        [FromServices] IRegisterRegionDDDUseCase useCase,
        [FromBody] RequestRegionDDDJson request)
    {
        var result = await useCase.RegisterDDDAsync(request);

        return Ok(result);
    }
}
