using AutoMapper;
using Region.Persistence.Communication;
using Region.Persistence.Communication.Request;

namespace Region.Persistence.Application.Services.Automapper;
public class TechChallengeProfile : Profile
{
    public TechChallengeProfile()
    {
        RequestToEntity();
    }

    private void RequestToEntity()
    {
        CreateMap<RequestRegionDDDJson, Domain.Entities.RegionDDD>()
            .ForMember(destiny => destiny.Region, config => config.MapFrom(origin => origin.Region.GetDescription()));
    }
}
