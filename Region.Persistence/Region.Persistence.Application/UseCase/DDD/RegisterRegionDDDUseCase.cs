using AutoMapper;
using FluentValidation.Results;
using MediatR;
using Region.Persistence.Application.Services.LoggedUser;
using Region.Persistence.Communication;
using Region.Persistence.Communication.Request;
using Region.Persistence.Communication.Response;
using Region.Persistence.Domain.Messages.DomaiEvents;
using Region.Persistence.Domain.Services;
using Region.Persistence.Exceptions;
using Region.Persistence.Exceptions.ExceptionBase;
using Serilog;
using TokenService.Manager.Controller;

namespace Region.Persistence.Application.UseCase.DDD;
public class RegisterRegionDDDUseCase(
    IRegionQueryServiceApi regionQueryServiceApi,
    IMediator mediator,
    IMapper mapper,
    ILoggedUser loggedUser,
    ILogger logger,
    TokenController tokenController) : IRegisterRegionDDDUseCase
{
    private readonly IRegionQueryServiceApi _regionQueryServiceApi = regionQueryServiceApi;
    private readonly IMediator _mediator = mediator;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger _logger = logger;
    private readonly TokenController _tokenController = tokenController;
    private readonly ILoggedUser _loggedUser = loggedUser;

    public async Task<Result<MessageResult>> RegisterDDDAsync(RequestRegionDDDJson request)
    {
        var output = new Result<MessageResult>();

        try
        {
            _logger.Information($"Start {nameof(RegisterDDDAsync)}. DDD: {request.DDD}.");

            var loggedUser = await _loggedUser.RecoverUser();

            var token = _tokenController.GenerateToken(loggedUser.Email);

            await Validate(request, token);

            var entity = _mapper.Map<Domain.Entities.RegionDDD>(request);

            entity.UserId = loggedUser.Id;


            await _mediator.Publish(new RegionCreateDomainEvent(
                Guid.NewGuid(),
                request.DDD,
                request.Region.GetDescription(),
                loggedUser.Id)
            );

            _logger.Information($"End {nameof(RegisterDDDAsync)}. User: {request.DDD}.");

            return output.Success(new MessageResult("Cadastro em processamento."));
        }
        catch (ValidationErrorsException ex)
        {
            var errorMessage = $"There are validations errors: {string.Concat(string.Join(", ", ex.ErrorMessages), ".")}";

            _logger.Error(ex, errorMessage);

            return output.Failure(ex.ErrorMessages);
        }
        catch (Exception ex)
        {
            var errorMessage = string.Format("There are an error: {0}", ex.Message);

            _logger.Error(ex, errorMessage);

            return output.Failure(new List<string>() { errorMessage });
        }
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
