using OpenBreviary.Core;
using Microsoft.Extensions.DependencyInjection;

namespace OpenBreviary.Tests
{
  public class OfficeServiceTests
  {

    [Fact]
    public void DI_Container_resolves_OfficeService()
    {
      //Given
      var services = new ServiceCollection();
      services.AddSingleton<LiturgicalCalculator>();
      services.AddTransient<OfficeService>();
      var provider = services.BuildServiceProvider();

      var service = provider.GetRequiredService<OfficeService>();

      Assert.NotNull(service);
    }

    [Theory]
    [InlineData("2010-12-25", LiturgicalSeason.ChristmasOctave)]
    [InlineData("2026-02-18", LiturgicalSeason.Lent)]
    [InlineData("2026-02-17", LiturgicalSeason.OrdinaryTime)]
    [InlineData("2026-02-25", LiturgicalSeason.Lent)]
    [InlineData("2026-03-29", LiturgicalSeason.HolyWeek)]
    [InlineData("2026-04-03", LiturgicalSeason.EasterTriduum)]
    [InlineData("2026-04-05", LiturgicalSeason.EasterOctave)]
    [InlineData("2026-08-05", LiturgicalSeason.OrdinaryTime)]
    public void ReturnCorrectLiturgicalSeason(string date, LiturgicalSeason expected)
    {
      var calculator = new LiturgicalCalculator();
      var service = new OfficeService(calculator);
      var ds = DateTime.Parse(date);

      var result = service.GetLiturgicalDay(ds).Season;

      Assert.Equivalent(expected, result);
    }

    [Theory]
    [InlineData("2010-12-25", 1)]
    [InlineData("2026-02-18", 0)]
    [InlineData("2026-02-17", 6)]
    [InlineData("2026-02-25", 1)]
    [InlineData("2026-03-29", 6)]
    [InlineData("2026-04-03", 6)]
    [InlineData("2026-04-05", 1)]
    public void ReturnsWeekOfSeason(string date, int expectWeekOfSeason)
    {
      var calculator = new LiturgicalCalculator();
      var service = new OfficeService(calculator);
      var ds = DateTime.Parse(date);

      var result = service.GetLiturgicalDay(ds).WeekOfSeason;

      Assert.Equal(expectWeekOfSeason, result);
    }


    [Theory]
    [InlineData("2026-03-29", 2)]
    [InlineData("2026-03-22", 1)]
    public void ReturnsCorrectPsalterWeek(string date, int expectedPsalterWeek)
    {
      var calculator = new LiturgicalCalculator();
      var service = new OfficeService(calculator);
      var ds = DateTime.Parse(date);

      var result = service.GetLiturgicalDay(ds).PsalterWeek;

      Assert.Equal(expectedPsalterWeek, result);
    }
  }
}
