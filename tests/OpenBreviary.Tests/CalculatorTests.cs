using OpenBreviary.Core;

namespace OpenBreviary.Tests;

public class CalculatorTests
{
  [Theory]
  [InlineData(2024, "2024-03-31")]
  [InlineData(2025, "2025-04-20")]
  [InlineData(2026, "2026-04-05")]
  public void ReturnsEasterDay(int year, string actualDate)
  {
    // Arrange
    var easterDay = DateTime.Parse(actualDate);
    var calculator = new LiturgicalCalculator();

    // Act
    var result = calculator.CalculateEaster(year);

    // Assert
    Assert.Equal(easterDay, result);
  }

  [Theory]
  [InlineData(2026, "2025-11-30")]
  [InlineData(2030, "2029-12-02")]
  [InlineData(2021, "2020-11-29")]
  public void ReturnsFirstSundayOfAdvent(int year, string actualDate)
  {
    var calculator = new LiturgicalCalculator();
    var expected = DateTime.Parse(actualDate);

    var result = calculator.FirstSundayOfAdvent(year);

    // Assert
    Assert.Equal(expected, result);
  }

}
