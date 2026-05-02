using DeveloperStore.Domain.Entities;
using FluentAssertions;

namespace DeveloperStore.UnitTests.Domain;

public class SaleTests
{
    [Fact]
    public void Should_Not_Update_Cancelled_Sale()
    {
        var sale = new Sale("Roger", "BH");
        sale.Cancel();

        var action = () => sale.Update("New", "NewBranch");

        action.Should().Throw<InvalidOperationException>();
    }
}