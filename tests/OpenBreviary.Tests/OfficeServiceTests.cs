using OpenBreviary.Core;

namespace OpenBreviary.Tests
{
  public class OfficeServiceTests
  {
    [Theory]
    [InlineData("2010-12-25", LiturgicalSeason.Christmastide)]
    [InlineData("2026-02-18", LiturgicalSeason.Lent)]
    public void ReturnsLiturgicalDay(string date, LiturgicalSeason expected)
    {
      var calculator = new LiturgicalCalculator();
      var service = new OfficeService(calculator);
      var ds = DateTime.Parse(date);

      var result = service.GetLiturgicalDay(ds).Season;

      Assert.Equivalent(expected, result);
    }

    [Fact]
    public void DI_Container_resolves_OfficeService()
    {
      //Given
      var services = new ServiceCollection();
      services.AddSingleton<LiturgicalCalculator>();
      services.AddTransient<OfficeService>();
      var provider = services.BuldServiceProvider();

      var service = provider.GetRequiredService<OfficeService>();

      Assert.NotNull(service);
    }
  }
}
