using CardCollector_backend.Dtos.Cards;
using CardCollector_backend.Models;
using CardCollector_backend.Repositories;
using CardCollector_backend.Services;
using CardCollector_backend.Tests.Fixtures;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Xunit;

namespace CardCollector_backend.Tests.Services;

public class CardServiceTests : IClassFixture<InMemoryFixture>
{
    private readonly InMemoryFixture _fixture;

    public CardServiceTests(InMemoryFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task CardService_GetCard_ReturnsCard()
    {
        //Arrange
        _fixture.Reset();
        CardService sut = new(new CardRepository(_fixture._context));

        //Act
        var result = await sut.GetCard(1);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<GetCardResponseDto>(result);

    }

    [Fact]
    public async Task CardService_GetAllCards_ReturnsCardCollection()
    {
        //Arrange
        _fixture.Reset();
        CardService sut = new(new CardRepository(_fixture._context));

        //Act
        var result = await sut.GetCards();

        //Assert
        Assert.NotNull(result);
        Assert.IsType<IEnumerable<GetCardResponseDto>>(result, exactMatch: false);
    }

    [Fact]
    public async Task CardService_DeleteCard_ReturnsNull()
    {
        //Arrange
        _fixture.Reset();
        CardService sut = new(new CardRepository(_fixture._context));

        //Act
        await sut.DeleteCard(1);
        var result = await sut.GetCard(1);

        //Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task CardService_AddCard_ReturnsCard()
    {
        //Arrange
        _fixture.Reset();
        CardService sut = new(new CardRepository(_fixture._context));

        CreateCardRequestDto testCard = new()
        {
            Name = "TestCard",
            Rarity = Enums.CardRarity.Common
        };
        //Act
        var result = await sut.AddCard(testCard);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<GetCardResponseDto>(result);
        Assert.Equal(testCard.Name, result.Name);

    }

    [Fact]
    public async Task CardService_UpdateCard_ReturnsCard()
    {
        //Arrange
        _fixture.Reset();
        CardService sut = new(new CardRepository(_fixture._context));

        UpdateCardRequestDto testCard = new()
        {
            Id = 1,
            Name = "UpdatedCard",
            Rarity = Enums.CardRarity.Uncommon
        };

        //Act
        var result = await sut.UpdateCard(testCard.Id, testCard);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<GetCardResponseDto>(result);
        Assert.Equal(testCard.Name, result.Name);
        Assert.Equal(testCard.Rarity, result.Rarity);
    }

    [Fact]
    public async Task CardService_UpdateCardInvalid_ReturnsNull()
    {
        //Arrange
        _fixture.Reset();
        CardService sut = new(new CardRepository(_fixture._context));

        UpdateCardRequestDto testCard = new()
        {
            Id = 1,
            Name = "UpdatedCard",
            Rarity = Enums.CardRarity.Uncommon
        };
        const long INVALID_ID = -1;

        //Act
        var result = await sut.UpdateCard(INVALID_ID, testCard);

        //Assert
        Assert.Null(result);
    }
}