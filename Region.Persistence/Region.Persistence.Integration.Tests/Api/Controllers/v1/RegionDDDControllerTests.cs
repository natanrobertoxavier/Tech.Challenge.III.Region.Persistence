using FluentAssertions;
using Region.Persistence.Integration.Tests.Fakes.Request;
using System.Net;

namespace Region.Persistence.Integration.Tests.Api.Controllers.v1;
public class RegionDDDControllerTests() : BaseTestClient("")
{
    private const string URI_REGION_DDD = "/api/v1/regionddd";
    private const string VALID_EMAIL = "natan@email.com";

    [Fact]
    public async Task RegionDDDController_Created_WhenDDDRegisteredSuccessfully()
    {
        // Arrange
        var token = GetValidToken(VALID_EMAIL);

        var request = new RequestRegionDDDJsonBuilder()
            .Build();

        // Act
        var response = await PostRequest(URI_REGION_DDD, request, token);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}
