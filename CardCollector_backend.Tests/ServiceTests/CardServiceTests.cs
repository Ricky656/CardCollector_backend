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
        CardService sut = new(new CardRepository(_fixture._context));

        //Act
        var result = await sut.GetCard(1);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<GetCardResponseDto>(result);

    }
}