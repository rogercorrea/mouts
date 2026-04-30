using DeveloperStore.Application.UseCases.CreateSale;
using DeveloperStore.Application.Interfaces;
using DeveloperStore.Domain.Entities;
using FluentAssertions;
using Moq;
using Xunit;

public class CreateSaleUseCaseTests
{
    [Fact]
    public async Task Should_Create_Sale_And_Save_In_Repository()
    {
        // Arrange
        var repoMock = new Mock<ISaleRepository>();

        var useCase = new CreateSaleUseCase(repoMock.Object);

        var request = new CreateSaleRequest
        {
            Customer = "Roger",
            Branch = "BH",
            Items = new List<CreateSaleItemRequest>
            {
                new() { ProductId = "p1", Quantity = 5, UnitPrice = 10 }
            }
        };

        // Act
        var result = await useCase.ExecuteAsync(request);

        // Assert
        result.SaleId.Should().NotBeEmpty();
        result.TotalAmount.Should().Be(45m); // 50 - 10%

        repoMock.Verify(r => r.AddAsync(It.IsAny<Sale>()), Times.Once);
    }
}