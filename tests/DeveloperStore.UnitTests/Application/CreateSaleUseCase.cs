using DeveloperStore.Application.UseCases.CreateSale;
using DeveloperStore.Application.Interfaces;
using DeveloperStore.Domain.Entities;
using FluentAssertions;
using Moq;

public class CreateSaleUseCaseTests
{
    [Fact]
    public async Task Should_Create_Sale_And_Save_In_Repository()
    {
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

        var result = await useCase.ExecuteAsync(request);

        result.SaleId.Should().NotBeEmpty();
        result.TotalAmount.Should().Be(45m);

        repoMock.Verify(r => r.AddAsync(It.IsAny<Sale>()), Times.Once);
    }
}