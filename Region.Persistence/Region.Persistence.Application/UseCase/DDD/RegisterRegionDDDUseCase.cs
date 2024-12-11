using AutoMapper;
using FluentValidation.Results;
using Region.Persistence.Communication.Request;
using Region.Persistence.Communication.Response;
using Region.Persistence.Application.Services.LoggedUser;
using Region.Persistence.Exceptions;
using Region.Persistence.Exceptions.ExceptionBase;
using Region.Persistence.Domain.Services;
using System.ComponentModel.DataAnnotations;
using TokenService.Manager.Controller;

namespace Region.Persistence.Application.UseCase.DDD;
public class RegisterRegionDDDUseCase(
    IRegionQueryServiceApi regionQueryServiceApi,
    //IRegionDDDWriteOnlyRepository regionDDDWriteOnlyRepository,
    IMapper mapper,
    ILoggedUser loggedUser,
    TokenController tokenController) : IRegisterRegionDDDUseCase
{
    private readonly IRegionQueryServiceApi _regionQueryServiceApi = regionQueryServiceApi;
    //private readonly IRegionDDDWriteOnlyRepository _regionDDDWriteOnlyRepository = regionDDDWriteOnlyRepository;
    private readonly IMapper _mapper = mapper;
    private readonly TokenController _tokenController = tokenController;
    private readonly ILoggedUser _loggedUser = loggedUser;

    public async Task<Result<MessageResult>> RegisterDDDAsync(RequestRegionDDDJson request)
    {
        var loggedUser = await _loggedUser.RecoverUser();

        var token = _tokenController.GenerateToken(loggedUser.Email);

        await Validate(request, token);

        var entity = _mapper.Map<Domain.Entities.RegionDDD>(request);


        entity.UserId = loggedUser.Id;

        //await _regionDDDWriteOnlyRepository.Add(entity);

        throw new NotImplementedException();
    }

    private async Task Validate(RequestRegionDDDJson request, string token)
    {
        var validator = new RegisterRegionDDDValidator();
        var result = validator.Validate(request);

        var thereIsDDDNumber = await _regionQueryServiceApi.ThereIsDDDNumber(request.DDD, token);

        if (thereIsDDDNumber.IsSuccess && thereIsDDDNumber.Data.ThereIsDDDNumber)
        {
            result.Errors.Add(new ValidationFailure("DDD", ErrorsMessages.ThereIsDDDNumber));
        }
        else if (!thereIsDDDNumber.IsSuccess)
        {
            result.Errors.Add(new FluentValidation.Results.ValidationFailure("responseApi", $"{thereIsDDDNumber.Error}"));
        }

        if (!result.IsValid)
        {
            var messageError = result.Errors.Select(error => error.ErrorMessage).Distinct().ToList();
            throw new ValidationErrorsException(messageError);
        }
    }
}
