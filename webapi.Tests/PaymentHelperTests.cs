using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;
using webapi.Service; 
using webapi.Model;   
using webapi.Interfaces;
using webapi.Helpers;

public class PaymentHelperTests
{
    public PaymentHelperTests()
    {

    }


     [Fact]
    public void CalculateNetWithholdingTax_ShouldReturnCorrectNetTotal_WhenValidInputsProvided()
    {
        // Arrange
        double netTotal = 1000.00;
        double withholdingTax = 100.00;
        double expectedNet = 900.00;

        // Act
        double actualNet = PaymentHelper.CalculateNetWithholdingTax(netTotal, withholdingTax);

        // Assert
        Assert.Equal(expectedNet, actualNet);
    }

    [Fact]
    public void CalculateNetWithholdingTax_ShouldHandleZeroWithholdingTax()
    {
        // Arrange
        double netTotal = 500.00;
        double withholdingTax = 0.00;
        double expectedNet = 500.00;

        // Act
        double actualNet = PaymentHelper.CalculateNetWithholdingTax(netTotal, withholdingTax);

        // Assert
        Assert.Equal(expectedNet, actualNet);
    }

    [Fact]
    public void CalculateNetWithholdingTax_ShouldHandleNegativeNetTotal()
    {
        // Arrange
        double netTotal = -100.00;
        double withholdingTax = 50.00;
        double expectedNet = -150.00;

        // Act
        double actualNet = PaymentHelper.CalculateNetWithholdingTax(netTotal, withholdingTax);

        // Assert
        Assert.Equal(expectedNet, actualNet);
    }

}
