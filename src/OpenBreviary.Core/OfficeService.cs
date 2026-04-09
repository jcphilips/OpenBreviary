namespace OpenBreviary.Core
{
  public class OfficeService(LiturgicalCalculator calculator, LiturgicalConfiguration config = null)
  {
    public LiturgicalTime GetLiturgicalDay(DateTime date)
    {
      var configToUse = config ?? new LiturgicalConfiguration();
      var year = calculator.GetLiturgicalYear(date);
      var easter = calculator.CalculateEaster(year);
      var feasts = calculator.CalculateFeasts(easter, configToUse);
      var season = calculator.GetLiturgicalSeason(date, feasts, configToUse);

      var context = new LiturgicalContext(date, season, feasts, year, configToUse);

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
