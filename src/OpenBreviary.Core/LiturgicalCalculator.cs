namespace OpenBreviary.Core
{
  public class LiturgicalCalculator
  {
    // The liturgical calendar starts 4 weeks
    // Before the Nativity of the Lord

    const int DAYSINAWEEK = 7;

    // Uses Meeus/Jones/Butcher Gregorian Easter Algorithm 
    public DateTime CalculateEaster(int year)
    {
      int a = year % 19;
      int b = year / 100;
      int c = year % 100;
      int d = b / 4;
      int e = b % 4;
      int f = (b + 8) / 25;
      int g = (b - f + 1) / 3;
      int h = ((19 * a) + b - d - g + 15) % 30;
      int i = c / 4;
      int k = c % 100 % 4; // Simplified: c % 4
      int l = (32 + (2 * e) + (2 * i) - h - k) % 7;
      int m = (a + (11 * h) + (22 * l)) / 451;

      int month = (h + l - (7 * m) + 114) / 31;
      int day = ((h + l - (7 * m) + 114) % 31) + 1;

      return new DateTime(year, month, day);
    }

    public DateTime FirstSundayOfAdvent(int year)
    {
      DateTime christmas = new DateTime(year, 12, 25);
      var FirstSunday = christmasDay.DayOfWeek == DayOfWeek.Sunday ? christmasDay.AddDays(-4 * DAYSINAWEEK) :
        christmasDay.AddDays(-(christmasDay.DayOfWeek - DayOfWeek.Sunday) - (3 * DAYSINAWEEK));
      return FirstSunday;
    }

  }
}
