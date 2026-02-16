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
      DateTime christmas = new(year, 12, 25);

      return christmas.DayOfWeek == DayOfWeek.Sunday ? christmas.AddDays(-4 * DAYSINAWEEK) :
        christmas.AddDays(-(christmas.DayOfWeek - DayOfWeek.Sunday) - (3 * DAYSINAWEEK));
    }

    public DateTime GetStartOfOrdinaryTime(int year)
    {
      DateTime baptism = GetBaptismOfTheLord(year);

      // Ordinary Time starts the day after the Baptism of the Lord
      // i.e. the following Monday
      return baptism.AddDays(1);
    }

    public DateTime GetBaptismOfTheLord(int year)
    {
      // The Baptism of the Lord is on the Sunday
      // after the Epiphany
      DateTime jan2 = new(year, 1, 2);

      // Look for the next Sunday
      // which will be the Epiphany
      int daysUntilSunday = ((int)DayOfWeek.Sunday - (int)jan2.DayOfWeek + 7) % 7;

      daysUntilSunday += 7;

      return jan2.AddDays(daysUntilSunday);
    }

    public MoveableFeasts CalculateFeasts(DateTime easter)
    {
      return new MoveableFeasts(
          AshWednesday: easter.AddDays(-46),
          ThursdayAfterAshWednesday: easter.AddDays(-45),
          FridayAfterAshWednesday: easter.AddDays(-44),
          SaturdayAfterAshWednesday: easter.AddDays(-43),
          PalmSunday: easter.AddDays(-DAYSINAWEEK),
          HolyThursday: easter.AddDays(-3),
          GoodFriday: easter.AddDays(-2),
          HolySaturday: easter.AddDays(-1),
          Easter: easter,
          DivineMercy: easter.AddDays(7),
          Ascension: easter.AddDays(39),
          Pentecost: easter.AddDays(49),
          MostHolyTrinity: easter.AddDays(56),
          TheBodyAndBloodOfChrist: easter.AddDays(60),
          TheMostSacredHeartOfJesus: easter.AddDays(68)
          );
    }

    public LiturgicalSeason GetLiturgicalSeason(DateTime date)
    {
      var easter = CalculateEaster(date.Year);
      MoveableFeasts feasts = CalculateFeasts(easter);
      DateTime firstSundayOfAdvent = FirstSundayOfAdvent(date.Year);
      DateTime christmas = new(date.Year, 12, 25);
      DateTime solemnityOfMaryMotherOfGod = new(date.Year, 01, 01);
      DateTime firstMondayOfOrdinaryTime = GetStartOfOrdinaryTime(date.Year);

      if (date >= feasts.AshWednesday && date < feasts.PalmSunday)
      {
        return LiturgicalSeason.Lent;
      }
      if (date >= feasts.PalmSunday && date < feasts.HolyThursday)
      {
        return LiturgicalSeason.HolyWeek;
      }
      if (date >= feasts.HolyThursday && date < feasts.Easter)
      {
        return LiturgicalSeason.EasterTriduum;
      }
      if (date >= feasts.Easter && date < feasts.DivineMercy)
      {
        return LiturgicalSeason.EasterOctave;
      }
      if (date >= feasts.DivineMercy && date <= feasts.Pentecost)
      {
        return LiturgicalSeason.Eastertide;
      }
      if (date >= firstSundayOfAdvent && date < christmas)
      {
        return LiturgicalSeason.Advent;
      }
      if (date >= christmas || (date >= solemnityOfMaryMotherOfGod && date < firstMondayOfOrdinaryTime))
      {
        return LiturgicalSeason.Christmastide;
      }
      return LiturgicalSeason.OrdinaryTime;
    }
  }
}
