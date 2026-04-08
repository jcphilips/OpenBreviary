namespace OpenBreviary.Core
{
  public class LiturgicalCalculator
  {
    const int DAYS_IN_A_WEEK = 7;
    const int FOUR_WEEK_PSALTER = 4;

    public int GetLiturgicalYear(DateTime date)
    {
      var currentYear = date.Year;
      var firstSundayOfAdvent = GetFirstSundayOfAdvent(currentYear);
      return date >= firstSundayOfAdvent ? currentYear + 1 : currentYear;
    }

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

    // The liturgical calendar starts 4 weeks
    // Before the Nativity of the Lord
    public DateTime GetFirstSundayOfAdvent(int year)
    {
      DateTime christmas = new(year, 12, 25);

      return christmas.DayOfWeek == DayOfWeek.Sunday ? christmas.AddDays(-4 * DAYS_IN_A_WEEK) :
        christmas.AddDays(-(christmas.DayOfWeek - DayOfWeek.Sunday) - (3 * DAYS_IN_A_WEEK));
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
          PalmSunday: easter.AddDays(-DAYS_IN_A_WEEK),
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

    public LiturgicalSeason GetLiturgicalSeason(DateTime date, MoveableFeasts feasts)
    {
      DateTime Christmas = new(date.Year, 12, 25);
      DateTime FirstSundayOfAdvent = GetFirstSundayOfAdvent(date.Year);
      DateTime SolemnityOfMaryMotherOfGod = new(date.Year, 01, 01);
      DateTime FirstMondayOfOrdinaryTime = GetStartOfOrdinaryTime(date.Year);

      if (date >= SolemnityOfMaryMotherOfGod && date < FirstMondayOfOrdinaryTime)
      {
        return LiturgicalSeason.Christmastide;
      }
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
      if (date >= FirstSundayOfAdvent && date < Christmas)
      {
        return LiturgicalSeason.Advent;
      }
      if (date >= Christmas)
      {
        return LiturgicalSeason.ChristmasOctave;
      }
      return LiturgicalSeason.OrdinaryTime;
    }

    public int GetWeekOfSeason(LiturgicalContext context)
    {
      var season = context.Season;
      var currentDate = context.Date;
      var feasts = context.Feasts;


      if (season is LiturgicalSeason.Advent)
      {
        var firstSundayOfAdvent = GetFirstSundayOfAdvent(currentDate.Year);
        var daysSinceStartOfAdvent = (currentDate - firstSundayOfAdvent).Days;
        return (daysSinceStartOfAdvent / DAYS_IN_A_WEEK) + 1;
      }
      if (season is LiturgicalSeason.ChristmasOctave)
      {
        // Throughout the octave of Christmas, Evening Prayer is of the octave,
        // as given below for each day. Solemntities and Sunday of the Holy Family 
        // are exceptions to this directive.
        //
        // Throughout the octave of Christmas, either form of Night Prayer for Sunday is used each evening.
        //
        // Sunday within the Octave of Christmas - Feast of the Holy Family (Psalter Week 1)
        // If Christmas falls on a Sunday, the Feast of the Holy Family is celebrated on the 30th and has
        // no Evening Prayer I.
        // 26th - Lauds of St Stehphen if not a Sunday
        // 27th - Lauds of St John, Apostle and Evangelist if not a Sunday
        // 28th - Lauds of Holy Innocents if not a Sunday
        return 1;
      }
      if (season is LiturgicalSeason.Christmastide)
      {
        // The manner of celebrating this time depends on the day chosen by the Region for the Solemnity 
        // of the Epiphany. The offices are set out according to 2 possibilities.
        // A: Epiphany is celebrated on the 6th of January
        // B: Epihany is celebrated on the Sunday between the 2nd and 8th of January
        // AB: Offices celebrated in all regions
        // A(B): Offices may be celebrated in all regions but vary year to year.

        return 2;
      }
      if (season is LiturgicalSeason.Lent)
      {
        var FirstSundayOfLent = feasts.AshWednesday.AddDays(4);
        if (currentDate < FirstSundayOfLent)
        {
          // Start of Lent has its own Psalter and prayers
          return 0;
        }

        var daysSinceFirstSundayOfLent = (currentDate - FirstSundayOfLent).Days;
        return (daysSinceFirstSundayOfLent / DAYS_IN_A_WEEK) + 1;
      }

      if (season is LiturgicalSeason.HolyWeek || season is LiturgicalSeason.EasterTriduum) return 6;


      if (season is LiturgicalSeason.EasterOctave)
      {
        return 1;
      }

      if (season is LiturgicalSeason.Eastertide)
      {
        var daysSinceEaster = (currentDate - feasts.Easter).Days;
        return (daysSinceEaster / DAYS_IN_A_WEEK) + 1;
      }
      if (season is LiturgicalSeason.OrdinaryTime && currentDate < feasts.AshWednesday)
      {
        var firstMondayOfOrdinaryTime = GetStartOfOrdinaryTime(currentDate.Year);
        var precedingSunday = firstMondayOfOrdinaryTime.AddDays(-1);
        return (currentDate - precedingSunday).Days / DAYS_IN_A_WEEK;
      }

      if (season is LiturgicalSeason.OrdinaryTime && currentDate > feasts.Easter)
      {
        int daysSinceSunday = (int)currentDate.DayOfWeek;
        var currentSunday = currentDate.AddDays(-daysSinceSunday);
        var firstSundayOfAdvent = GetFirstSundayOfAdvent(currentDate.Year);
        var weeksTillAdvent = (firstSundayOfAdvent - currentSunday).Days / DAYS_IN_A_WEEK;
        return 34 - (weeksTillAdvent - 1);
      }
      // if for whatever reason we fail
      return -1;
    }

    public int CalculatePsalterWeek(LiturgicalContext context)
    {
      var season = context.Season;
      var weekOfSeason = GetWeekOfSeason(context);

      if (season is LiturgicalSeason.Lent && weekOfSeason == 0) return 4;
      if (weekOfSeason == 0) throw new Exception("Invalid week of season.");

      return ((weekOfSeason - 1) % FOUR_WEEK_PSALTER) + 1;
    }
  }
}
