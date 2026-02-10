using OpenBreviary.Core;

namespace OpenBreviary.Tests;

public class CalculatorTests
{
  [Fact]
  public void ReturnsFifthSundayInOrdinaryTime()
  {
    // Arrange
    var calculator = new LiturgicalCalculator();
    var sundayInOrdinaryTime = new Date(2026, 2, 9);

    // Act
    var result = calculator.CalculateDay(sundayInOrdinaryTime);

    // Assert
    Assert.Equal("5th Sunday in Ordinary Time", result);
  }
}
