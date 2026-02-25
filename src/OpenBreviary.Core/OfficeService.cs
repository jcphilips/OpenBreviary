namespace OpenBreviary.Core
{
  public class OfficeService
  {
    private readonly LiturgicalCalculator _calculator;

    public OfficeService(LiturgicalCalculator calculator)
    {
      _calculator = calculator;
    }

    public LiturgicalTime GetLiturgicalDay(DateTime date)
    {
      var easter = _calculator.CalculateEaster(date.Year);
      var feasts = _calculator.CalculateFeasts(easter);
      var season = _calculator.GetLiturgicalSeason(date, feasts);

      return new LiturgicalTime
      {
        Date = date,
        Season = season,
        Description = $"Day in {season}"
      };
    }
  }
}
