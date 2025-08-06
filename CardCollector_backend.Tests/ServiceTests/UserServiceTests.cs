using FakeItEasy;
using CardCollector_backend.Tests.Fixtures;
using CardCollector_backend.Services.Interfaces;
using CardCollector_backend.Services;
using CardCollector_backend.Repositories;
using Microsoft.AspNetCore.Http;
using CardCollector_backend.Dtos.Users;
using CardCollector_backend.Models;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace CardCollector_backend.Tests.Services;

public class UserServiceTests : IClassFixture<InMemoryFixture>
{
    private readonly InMemoryFixture _fixture;
    private readonly IUserCardService _userCardService;
    private readonly ITokenService _tokenService;
    private readonly HttpContext _httpContext;
    private readonly IUserService sut;

    public UserServiceTests(InMemoryFixture fixture)
    {
        _fixture = fixture;
        _userCardService = A.Fake<IUserCardService>();
        _tokenService = A.Fake<ITokenService>();
        _httpContext = A.Fake<HttpContext>();

        sut = new UserService(new UserRepository(_fixture._context), _userCardService, new PackRepository(_fixture._context), _tokenService);
    }

    [Fact]
    public async Task UserService_LogIn_ReturnsUser()
    {
        //Arrange
        _fixture.Reset();
        LoginUserDto testLogin = new()
        {
            Username = "TestUser",
            Email = "user@test.com",
            Password = "password"
        };
        A.CallTo(() => _tokenService.CreateUserTokens(A<User>._, A<HttpContext>._)).ReturnsLazily((User user, HttpContext context) => new LoginResponseUserDto()
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            Role = user.Role
        });


        //Act
        var result = await sut.Login(testLogin, _httpContext);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<LoginResponseUserDto>(result);
        Assert.Equal(testLogin.Username, result.Username);
    }
}