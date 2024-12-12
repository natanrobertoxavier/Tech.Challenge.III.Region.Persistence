using Region.Persistence.Communication.Request;
using Region.Persistence.Communication.Request.Enum;

namespace Region.Persistence.Integration.Tests.Fakes.Request;
public class RequestRegionDDDJsonBuilder
{
    private RegionRequestEnum _region = RegionRequestEnum.Sudeste;
    private int _dDD = 11;

    public RequestRegionDDDJson Build()
    {
        return new RequestRegionDDDJson()
        {
            Region = _region,
            DDD = _dDD
        };
    }

    public RequestRegionDDDJsonBuilder WithRegion(RegionRequestEnum value)
    {
        _region = value;
        return this;
    }

    public RequestRegionDDDJsonBuilder WithDDD(int value)
    {
        _dDD = value;
        return this;
    }
}
