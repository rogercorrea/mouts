using DeveloperStore.Application.UseCases.CancelSale;
using DeveloperStore.Application.Interfaces;
using DeveloperStore.Domain.Entities;
using FluentAssertions;
using Moq;

namespace DeveloperStore.UnitTests.Application;

public class CancelSaleUseCaseTests
{
    [Fact]
    public async Task Should_Cancel_Sale()
    {
        var repoMock = new Mock<ISaleRepository>();

        var sale = new Sale("Roger", "BH");

        repoMock
            .Setup(r => r.GetByIdAsync(sale.Id))
            .ReturnsAsync(sale);

        var useCase = new CancelSaleUseCase(repoMock.Object);

        await useCase.ExecuteAsync(new CancelSaleRequest
        {
            SaleId = sale.Id
        });

        sale.IsCancelled.Should().BeTrue();

        repoMock.Verify(r => r.UpdateAsync(sale), Times.Once);
    }

    [Fact]
    public async Task Should_Throw_When_Sale_Already_Cancelled()
    {
        var repoMock = new Mock<ISaleRepository>();

        var sale = new Sale("Roger", "BH");
        sale.Cancel();

        repoMock
            .Setup(r => r.GetByIdAsync(sale.Id))
            .ReturnsAsync(sale);

        var useCase = new CancelSaleUseCase(repoMock.Object);

        var action = async () => await useCase.ExecuteAsync(new CancelSaleRequest
        {
            SaleId = sale.Id
        });

        await action.Should()
            .ThrowAsync<InvalidOperationException>()
            .WithMessage("Sale is already cancelled");
    }
}