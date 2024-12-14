using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Region.Persistence.Application.Interfaces;
using Region.Persistence.Application.Messages;
using Region.Persistence.Application.Messages.Handlers;
using Region.Persistence.Application.Services;
using Region.Persistence.Application.Services.LoggedUser;
using Region.Persistence.Application.UseCase.DDD;
using Region.Persistence.Domain.Messages;
using Region.Persistence.Domain.Messages.DomaiEvents;
using TokenService.Manager.Controller;

namespace Region.Persistence.Application;

public static class Initializer
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        AddAdditionalKeyPassword(services, configuration);
        AddJWTToken(services, configuration);
        AddUseCases(services);
        AddLoggedUser(services);
        AddDomainEvents(services);
        AddEvents(services);
    }

    private static void AddAdditionalKeyPassword(IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetRequiredSection("Settings:Password:AdditionalKeyPassword");
        services.AddScoped(option => new PasswordEncryptor(section.Value));
    }

    private static void AddJWTToken(IServiceCollection services, IConfiguration configuration)
    {
        var sectionLifeTime = configuration.GetRequiredSection("Settings:Jwt:LifeTimeTokenMinutes");
        var sectionKey = configuration.GetRequiredSection("Settings:Jwt:KeyToken");
        services.AddScoped(option => new TokenController(int.Parse(sectionLifeTime.Value), sectionKey.Value));
    }

    private static void AddUseCases(IServiceCollection services)
    {
        services.AddScoped<IRegisterRegionDDDUseCase, RegisterRegionDDDUseCase>(); ;
    }
    private static void AddLoggedUser(IServiceCollection services)
    {
        services.AddScoped<ILoggedUser, LoggedUser>();
    }

    private static void AddDomainEvents(IServiceCollection services)
    {
        services.AddScoped<IMessagePublisher, MessagePublisher>();
        services.AddScoped<INotificationHandler<RegionCreateDomainEvent>, RegionEventHandler>();
    }

    private static void AddEvents(IServiceCollection services)
    {
        services.AddScoped<IEventAppService, EventsAppService>();
    }
}
