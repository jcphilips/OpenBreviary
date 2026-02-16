using OpenBreviary.Core;

namespace OpenBreviary.Tests
{
  public class OfficeService
  {
    [Theory]
    [InlineData("2010-12-25", LiturgicalSeason.Christmastide)]
    [InlineData("2026-02-18", LiturgicalSeason.Lent)]
    public void ReturnsLiturgicalSeason(string date, LiturgicalSeason expected)
    {
      DateTime ds = DateTime.Parse(date);
      LiturgicalCalculator calc = new();

      LiturgicalSeason season = calc.GetLiturgicalSeason(ds);

      Assert.Equivalent(expected, season);
    }
  }
}
