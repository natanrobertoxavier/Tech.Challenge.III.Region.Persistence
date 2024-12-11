using Newtonsoft.Json;

namespace Region.Persistence.Domain.ResultServices;
public class ThereIsDDDNumberResult
{
    [JsonProperty("thereIsDDDNumber")]
    public bool ThereIsDDDNumber { get; set; }
}
