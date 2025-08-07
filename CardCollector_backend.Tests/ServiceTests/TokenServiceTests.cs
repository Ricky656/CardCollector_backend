using Microsoft.Extensions.Configuration;
using CardCollector_backend.Repositories;
using CardCollector_backend.Services;
using CardCollector_backend.Services.Interfaces;
using CardCollector_backend.Tests.Fixtures;
using FakeItEasy;
using CardCollector_backend.Models;
using Microsoft.AspNetCore.Http;
using CardCollector_backend.Dtos.Users;

namespace CardCollector_backend.Tests.Services;

public class TokenServiceTests : IClassFixture<InMemoryFixture>
{
    private readonly InMemoryFixture _fixture;
    private readonly IConfiguration _config;
    private readonly HttpContext _context;
    private readonly ITokenService sut;

    public TokenServiceTests(InMemoryFixture fixture)
    {
        _fixture = fixture;
        _context = A.Fake<HttpContext>();
        Dictionary<String, String?> InMemoryAppSettings = new()
        {
            {"AppSettings:Secret", "asaudbfghdjbghsdabngiajsdfuh2uh4b2j35bu32b5u325j3b25u3b235ub2uienrwioenfi3b523232323ew"},
            {"AppSettings:Issuer", "CardCollector_backend"},
            {"AppSettings:Audience", "CardCollector_frontend"},
            {"AppSettings:TokenValidityMinutes", "1"},
            {"AppSettings:RefreshValidityDays" , "7"}
        };
        _config = new ConfigurationBuilder().AddInMemoryCollection(InMemoryAppSettings).Build();

        sut = new TokenService(_config, new UserRepository(_fixture._context));
    }

    [Fact]
    public async Task TokenService_CreateUserTokens_ReturnsUserLogin()
    {
        //Arrange
        _fixture.Reset();
        User testUser = new()
        {
            Id = 1,
            Username = "TestUser",
            Email = "TestEmail",
            Role = "User"
        };

        //Act
        var result = await sut.CreateUserTokens(testUser, _context);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<LoginResponseUserDto>(result);
        Assert.NotNull(result.Token);
    }
}