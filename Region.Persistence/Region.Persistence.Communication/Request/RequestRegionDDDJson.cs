using Region.Persistence.Communication.Request.Enum;

namespace Region.Persistence.Communication.Request;
public class RequestRegionDDDJson
{
    public RegionRequestEnum Region { get; set; }
    public int DDD { get; set; }
}
