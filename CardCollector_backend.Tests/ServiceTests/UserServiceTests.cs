using FakeItEasy;
using CardCollector_backend.Tests.Fixtures;
using CardCollector_backend.Services.Interfaces;
using CardCollector_backend.Services;
using CardCollector_backend.Repositories;
using Microsoft.AspNetCore.Http;
using CardCollector_backend.Dtos.Users;
using CardCollector_backend.Models;
using CardCollector_backend.Dtos.UserCards;

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

    [Fact]
    public async Task UserService_AddUser_ReturnsUser()
    {
        //Arrange
        _fixture.Reset();
        CreateUserRequestDto testUser = new()
        {
            Username = "testUser",
            Email = "testEmail",
            Password = "password"
        };

        //Act
        var result = await sut.AddUser(testUser);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<GetUserResponseDto>(result);
        Assert.Equal(testUser.Username, result.Username);
    }

    [Fact]
    public async Task UserService_DeleteUser_ReturnsNull()
    {
        //Arrange
        _fixture.Reset();

        //Act
        await sut.DeleteUser(1);
        var result = await sut.GetUser(1);

        //Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task UserService_GetUser_ReturnsUser()
    {
        //Arrange
        _fixture.Reset();

        //Act
        var result = await sut.GetUser(1);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<GetUserResponseDto>(result);
        Assert.Equal(1, result.Id);
    }

    [Fact]
    public async Task UserService_GetAllUsers_ReturnsUserCollection()
    {
        //Arrange
        _fixture.Reset();

        //Act
        var result = await sut.GetUsers();

        //Assert
        Assert.NotNull(result);
        Assert.IsType<IEnumerable<GetUserResponseDto>>(result, exactMatch: false);
    }

    [Fact]
    public async Task UserService_GetUserCards_ReturnsUserWithCards()
    {
        //Arrange
        _fixture.Reset();

        //Act
        var result = await sut.GetUserCards(1);

        //Assert
        Assert.NotNull(result);
        Assert.NotNull(result.UserCards);
        Assert.IsType<IEnumerable<GetUserCardResponseDto>>(result.UserCards, exactMatch: false);
    }

    [Fact]
    public async Task UserService_UpdateUser_ReturnsUser()
    {
        //Arrange
        _fixture.Reset();
        UpdateUserRequestDto updatedUser = new()
        {
            Username = "Updated Username"
        };

        //Act
        var result = await sut.UpdateUser(1, updatedUser);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<GetUserResponseDto>(result);
        Assert.Equal(updatedUser.Username, result.Username);
    }

    [Fact]
    public async Task UserService_UpdateUserInvalid_ReturnsNull()
    {
        //Arrange
        _fixture.Reset();
        UpdateUserRequestDto updatedUser = new()
        {
            Username = "Updated Username"
        };

        //Act
        var result = await sut.UpdateUser(-1, updatedUser);

        //Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task UserService_AddUserCard_ReturnsUserCard()
    {
        //Arrange
        _fixture.Reset();
        CreateUserCardRequestDto testUserCard = new()
        {
            UserId = 1,
            CardId = 1
        };
        A.CallTo(() => _userCardService.AddUserCard(A<UserCard>._)).ReturnsLazily((UserCard card) => new GetUserCardResponseDto()
        {
            Id = card.Id,
            CardId = card.CardId,
            UserId = card.UserId
        });

        //Act
        var result = await sut.AddUserCard(testUserCard);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(testUserCard.UserId, result.UserId);
        Assert.Equal(testUserCard.CardId, result.CardId);
    }

    [Fact]
    public async Task UserService_DeleteUserCard_ReturnsEmptyCollection()
    {
        //Arrange
        _fixture.Reset();

        //Act
        await sut.DeleteUserCard(1, 1);
        var result = await sut.GetUserCards(1);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<GetUserResponseDto>(result);
        Assert.Empty(result.UserCards);
    }

    [Fact]
    public async Task UserService_OpenPack_ReturnsCards()
    {
        //Arrange
        _fixture.Reset();
        const int CARDS_PER_PACK = 3; //Move to ENV variable? 

        //Act
        var result = await sut.OpenPack(1, 1); 

        //Assert
        Assert.NotNull(result);
        Assert.IsType<IEnumerable<GetUserCardResponseDto>>(result, exactMatch: false);
        Assert.Equal(CARDS_PER_PACK, result.Count());
    }
}