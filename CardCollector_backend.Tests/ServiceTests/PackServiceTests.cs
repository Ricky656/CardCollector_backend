using CardCollector_backend.Dtos.Packs;
using CardCollector_backend.Models;
using CardCollector_backend.Repositories;
using CardCollector_backend.Services;
using CardCollector_backend.Tests.Fixtures;

namespace CardCollector_backend.Tests.Services;

public class PackServiceTests : IClassFixture<InMemoryFixture>
{
    private readonly InMemoryFixture _fixture;
    private readonly PackService sut;

    public PackServiceTests(InMemoryFixture fixture)
    {
        _fixture = fixture;

        sut = new(new PackRepository(_fixture._context), new CardRepository(_fixture._context));
    }


    [Fact]
    public async Task PackService_GetPack_ReturnsPack()
    {
        //Arrange
        _fixture.Reset();

        //Act
        var result = await sut.GetPack(1);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<GetPackResponseDto>(result);
        Assert.Equal(1, result.Id);
    }

    [Fact]
    public async Task PackService_AddPack_ReturnsPack()
    {
        //Arrange
        _fixture.Reset();
        CreatePackRequestDto testPack = new()
        {
            Name = "TestPack",
            CardIds = [1]
        };

        //Act
        var result = await sut.AddPack(testPack);

        //Asssert
        Assert.NotNull(result);
        Assert.IsType<GetPackResponseDto>(result);
        Assert.Equal("TestPack", result.Name);
    }

    [Fact]
    public async Task PackService_DeletePack_ReturnsNull()
    {
        //Arrange
        _fixture.Reset();

        //Act
        await sut.DeletePack(1);
        var result = await sut.GetPack(1);

        //Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task PackService_UpdatePack_ReturnsPack()
    {
        //Arrange
        _fixture.Reset();
        UpdatePackRequestDto updatedPack = new()
        {
            Id = 1,
            Name = "UpdatedPack",
            CardIds = []
        };

        //Act
        var result = await sut.UpdatePack(1, updatedPack);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<GetPackResponseDto>(result);
        Assert.Equal([], result.Cards);
    }

    [Fact]
    public async Task PackService_UpdatePackInvalid_ReturnsNull()
    {
        //Arrange
        _fixture.Reset();
        UpdatePackRequestDto updatedPack = new()
        {
            Id = 1,
            Name = "UpdatedPack",
            CardIds = []
        };
        const long INVALID_ID = -1;

        //Act
        var result = await sut.UpdatePack(INVALID_ID, updatedPack);

        //Assert
        Assert.Null(result);
    }
}