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
  [InlineData(2025, "2025-11-30")]
  [InlineData(2029, "2029-12-02")]
  [InlineData(2020, "2020-11-29")]
  public void ReturnsFirstSundayOfAdvent(int year, string actualDate)
  {
    var calculator = new LiturgicalCalculator();
    var expected = DateTime.Parse(actualDate);

    var result = calculator.GetFirstSundayOfAdvent(year);

    // Assert
    Assert.Equal(expected, result);
  }

  [Theory]
  [InlineData(2026, "2026-01-12")]
  public void ReturnsStartOfOrdinaryTime(int year, string actualDate)
  {
    var calculator = new LiturgicalCalculator();
    var expected = DateTime.Parse(actualDate);

    var result = calculator.GetStartOfOrdinaryTime(year);

    // Assert
    Assert.Equal(expected, result);
  }

  [Theory]
  // In 2026, Jan 6 is Tuesday. Baptism of the Lord should be the following Sunday, Jan 11
  [InlineData(2026, "2026-01-11")]
  // In 2024, Jan 6 is Saturday. Baptism of the Lord should be the following Sunday, Jan 7
  [InlineData(2024, "2024-01-07")]
  public void ReturnsBaptismOfTheLord_WhenEpiphanyIsJan6(int year, string actualDate)
  {
    var calculator = new LiturgicalCalculator();
    var config = new LiturgicalConfiguration(EpiphanyTransferredToSunday: false);
    var expected = DateTime.Parse(actualDate);
    var result = calculator.GetBaptismOfTheLord(year, config);
    Assert.Equal(expected, result);
  }

  [Theory]
  // In 2026, Jan 4 is Sunday. Baptism of the Lord should be following Sunday, Jan 11 (Epiphany on Sun Jan 4)
  [InlineData(2026, "2026-01-11")]
  // In 2024, Jan 7 is Sunday. Baptism of the Lord should be following Monday, Jan 8 (Epiphany on Sun Jan 7)
  [InlineData(2024, "2024-01-08")]
  // In 2023, Jan 8 is Sunday. Baptism of the Lord should be following Monday, Jan 9 (Epiphany on Sun Jan 8)
  [InlineData(2023, "2023-01-09")]
  public void ReturnsBaptismOfTheLord_WhenEpiphanyIsSunday(int year, string actualDate)
  {
    var calculator = new LiturgicalCalculator();
    var config = new LiturgicalConfiguration(EpiphanyTransferredToSunday: true);
    var expected = DateTime.Parse(actualDate);
    var result = calculator.GetBaptismOfTheLord(year, config);
    Assert.Equal(expected, result);
  }

  [Theory]
  // 2026 Easter is April 5.
  [InlineData("2026-04-05", false, "2026-05-14")] // Thursday
  [InlineData("2026-04-05", true, "2026-05-17")]  // Sunday
  public void ReturnsAscension_BasedOnConfiguration(string easterStr, bool transferred, string actualAscension)
  {
    var calculator = new LiturgicalCalculator();
    var config = new LiturgicalConfiguration(AscensionTransferredToSunday: transferred);
    var easter = DateTime.Parse(easterStr);
    var expected = DateTime.Parse(actualAscension);
    var feasts = calculator.CalculateFeasts(easter, config);
    Assert.Equal(expected, feasts.Ascension);
  }

  [Fact]
  public void CalculatesPsalterWeek_Week1_OrdinaryTime_ReturnsWeek1()
  {
    var calculator = new LiturgicalCalculator();
    var config = new LiturgicalConfiguration();
    var date = new DateTime(2026, 1, 12);
    var easter = calculator.CalculateEaster(2026);
    var feasts = calculator.CalculateFeasts(easter, config);
    var season = LiturgicalSeason.OrdinaryTime;
    var context = new LiturgicalContext(date, season, feasts, 2026, config);

    var week = calculator.CalculatePsalterWeek(context);
    Assert.Equal(1, week);
  }

  [Fact]
  public void Christmastide_BeforeEpiphany_ReturnsWeek1_Or2()
  {
    var calculator = new LiturgicalCalculator();
    var config = new LiturgicalConfiguration(EpiphanyTransferredToSunday: true);
    var date = new DateTime(2023, 1, 5);
    var easter = calculator.CalculateEaster(2023);
    var feasts = calculator.CalculateFeasts(easter, config);
    var season = LiturgicalSeason.Christmastide;
    var context = new LiturgicalContext(date, season, feasts, 2023, config);

    var week = calculator.CalculatePsalterWeek(context);
    Assert.Equal(1, calculator.GetWeekOfSeason(context));
  }
}
