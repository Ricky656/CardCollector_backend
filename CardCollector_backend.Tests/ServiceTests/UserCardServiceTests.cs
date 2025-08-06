using CardCollector_backend.Dtos.UserCards;
using CardCollector_backend.Models;
using CardCollector_backend.Repositories;
using CardCollector_backend.Services;
using CardCollector_backend.Tests.Fixtures;

namespace CardCollector_backend.Tests.Services;

public class UserCardServicesTests : IClassFixture<InMemoryFixture>
{
    InMemoryFixture _fixture;

    public UserCardServicesTests(InMemoryFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task UserCardService_GetUserCards_ReturnsCards()
    {
        //Arrange
        _fixture.Reset();
        UserCardService sut = new(new UserCardRepository(_fixture._context), new CardRepository(_fixture._context));

        //Act
        var result = await sut.GetUserCards(1);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<IEnumerable<UserCard>>(result, exactMatch: false);
        Assert.Equal(1, result.FirstOrDefault()!.Id);
    }

    [Fact]
    public async Task UserCardService_AddUserCard_ReturnsUserCard()
    {
        //Arrange
        _fixture.Reset();
        UserCardService sut = new(new UserCardRepository(_fixture._context), new CardRepository(_fixture._context));
        UserCard testUserCard = new()
        {
            UserId = 1,
            CardId = 1
        };

        //Act
        var result = await sut.AddUserCard(testUserCard);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<GetUserCardResponseDto>(result);
        Assert.Equal(2, result.Id);
    }

    [Fact]
    public async Task UserCardService_DeleteUserCard_ReturnsNull()
    {
        //Arrange
        _fixture.Reset();
        UserCardService sut = new(new UserCardRepository(_fixture._context), new CardRepository(_fixture._context));

        //Act
        await sut.DeleteUserCard(1, 1);
        var result = await sut.GetUserCard(1);

        //Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task UserCardService_DeleteUserCardInvalid_ReturnsNull()
    {
        //Arrange
        _fixture.Reset();
        UserCardService sut = new(new UserCardRepository(_fixture._context), new CardRepository(_fixture._context));
        const long INVALID_ID = -1;

        //Act
        var result = await sut.DeleteUserCard(INVALID_ID, 1);

        //Assert
        Assert.Null(result);
    }
}