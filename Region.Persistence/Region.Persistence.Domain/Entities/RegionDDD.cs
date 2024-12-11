namespace Region.Persistence.Domain.Entities;
public class RegionDDD : BaseEntity
{
    public int DDD { get; set; }
    public string Region { get; set; }
    public Guid UserId { get; set; }
}
