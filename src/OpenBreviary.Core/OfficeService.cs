namespace OpenBreviary.Core
{
  public class OfficeService(LiturgicalCalculator calculator)
  {
    public LiturgicalTime GetLiturgicalDay(DateTime date)
    {
      var year = calculator.GetLiturgicalYear(date);
      var easter = calculator.CalculateEaster(year);
      var feasts = calculator.CalculateFeasts(easter);
      var season = calculator.GetLiturgicalSeason(date, feasts);

      var context = new LiturgicalContext(date, season, feasts, year);

      return new LiturgicalTime
      {
        Date = date,
        Season = season,
        WeekOfSeason = calculator.GetWeekOfSeason(context),
        PsalterWeek = calculator.CalculatePsalterWeek(context),
      };
    }
  }
}
